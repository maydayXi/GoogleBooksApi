using ApiInfrastructure.ExternalServices;
using ApiInfrastructure.Options;
using ApiService.Interface;
using GoogleBookApi.Helper;
using GoogleBookApi.ViewModels.Components;
using Serilog;
using Serilog.Events;

#region Serilog 
string? homePath = Environment.GetEnvironmentVariable("HOME") ??
    Environment.GetEnvironmentVariable("home");

string logPath = !string.IsNullOrEmpty(homePath)
    ? Path.Combine(homePath, "LogFiles", "Application", "error-.log")
    : Path.Combine(AppContext.BaseDirectory, "logs", "error-.log");

string logDirectory = Path.GetDirectoryName(logPath) ?? string.Empty;

if (!Directory.Exists(logDirectory))
    Directory.CreateDirectory(logDirectory);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: logDirectory,
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Error,
        fileSizeLimitBytes: 10 * 1024 * 1024, // 10 MB
        rollOnFileSizeLimit: true,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {SourceContext} - {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();
#endregion

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // Define a constant for the Google Books API option section name
    string GoogleBooks = nameof(GoogleBooks);

    builder.Services
        .Configure<GoogleBooksOptions>(builder.Configuration.GetSection(GoogleBooks))
        .AddScoped<IGoogleBookService, GoogleBookService>()
        .AddSingleton<IJsonDataProvider<GuidelineItemVm>, AppDataProvider<GuidelineItemVm>>()
        .AddSingleton<IJsonDataProvider<VersionItemVm>, AppDataProvider<VersionItemVm>>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    // API Controller Attributes Routing
    app.MapControllers();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}