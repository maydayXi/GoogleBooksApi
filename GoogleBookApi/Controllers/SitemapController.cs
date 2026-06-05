using System.Text;
using System.Xml.Linq;
using ApiService.Enums;
using ApiService.Extensions;
using GoogleBookApi.Helper;
using GoogleBookApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleBookApi.Controllers;

[Route("sitemap.xml")]
public class SitemapController : Controller
{
    [HttpGet, Produces("application/xml")]
    public IActionResult Index()
    {
        string baseUrl = $"{Request.Scheme}://{Request.Host}",
            homeController = WebHelper.GetControllerName<HomeController>();

        List<SitemapUrl> urls =
        [
            new(Loc: $"{baseUrl}/",
                ChangeFreq: ChangeFrequency.Monthly.GetDescription(),
                Priority: $"{1.0}"),
            new(Loc: $"{baseUrl}/{homeController}/{nameof(HomeController.BookSearch)}",
                ChangeFreq: ChangeFrequency.Monthly.GetDescription(),
                Priority: $"{0.9}"),
            new(Loc: $"{baseUrl}/{homeController}/{nameof(HomeController.Guideline)}",
                ChangeFreq: ChangeFrequency.Monthly.GetDescription(),
                Priority: $"{0.7}"),
            new(Loc: $"{baseUrl}/{homeController}/{nameof(HomeController.Version)}",
                ChangeFreq: ChangeFrequency.Monthly.GetDescription(),
                Priority: $"{0.3}")
        ];

        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        
        XDocument xmlDocument = new(
            new XDeclaration($"{1.0}", "UTF-8", null),
            new XElement(ns + "urlset", 
                urls.Select(url => new XElement(ns + "url",
                    new XElement(ns + $"{nameof(url.Loc).ToLower()}", url.Loc),
                    new XElement(ns + $"{nameof(url.ChangeFreq).ToLower()}", url.ChangeFreq),
                    new XElement(ns + $"{nameof(url.Priority).ToLower()}", url.Priority)
                    ))
                )
        );

        return Content($"{xmlDocument}", "application/xml");
    }
}