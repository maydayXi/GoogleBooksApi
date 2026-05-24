using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GoogleBookApi.Extensions;

/// <summary>
/// Provides extension methods for rendering partial views to HTML strings within a controller context.
/// </summary>
public static class ControllerExtensions
{
    /// <summary>
    /// Renders a partial view to an HTML string using the specified view name and model.
    /// </summary>
    /// <param name="controller">The controller instance used to provide the rendering context.</param>
    /// <param name="viewName">The name of the partial view to render.</param>
    /// <param name="model">The model to pass to the partial view.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the rendered HTML string.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the view engine cannot be resolved.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the specified view cannot be found.</exception>
    public static async Task<string> RenderPartialViewAsync(this Controller controller, string viewName, object model)
    {
        controller.ViewData.Model = model;

        var viewEngine = controller.HttpContext.RequestServices
            .GetRequiredService<ICompositeViewEngine>();

        var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

        if (!viewResult.Success)
            throw new InvalidOperationException($"Could not find view '{viewName}'.");

        // HTML output target
        await using var writer = new StringWriter();

        var viewContext = new ViewContext(
            controller.ControllerContext,
            viewResult.View,        // target view
            controller.ViewData,    // view data 
            controller.TempData,    // temp data
            writer,
            new HtmlHelperOptions()
        );

        await viewResult.View.RenderAsync(viewContext);
        return writer.ToString();
    }
}