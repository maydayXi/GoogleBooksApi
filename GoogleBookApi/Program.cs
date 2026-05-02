using ApiInfrastructure.ExternalServices;
using ApiInfrastructure.Options;
using ApiService.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Define a constant for the Google Books API option section name
string GoogleBooks = nameof(GoogleBooks);

builder.Services
    .Configure<GoogleBooksOptions>(builder.Configuration.GetSection(GoogleBooks))
    .AddScoped<IGoogleBookService, GoogleBookService>();

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
