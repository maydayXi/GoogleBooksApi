using GoogleBookApi.Models;
using GoogleBookApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoogleBookApi.Controllers;

/// <summary>
/// Controller for handling requests related to the application's home and informational pages.
/// </summary>
/// <param name="logger">The logger used to record application events.</param>
public class HomeController(ILogger<HomeController> logger) : Controller
{
    /// <summary>
    /// Provides logging capabilities for the HomeController.
    /// </summary>
    private readonly ILogger<HomeController> _logger = logger;

    /// <summary>
    /// Displays the home page of the application.
    /// </summary>
    /// <returns>
    /// A <see cref="IActionResult"/> that renders the home page view.
    /// </returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Renders the Guideline view.
    /// </summary>
    /// <returns>A <see cref="IActionResult"/> that renders the home page view.</returns>
    public IActionResult Guideline()
    {
        List<GuidelineItemVm> guidelineItems = [
            new() {
                Icon = "bi bi-google",
                Title = "資料來源",
                Description = "使用 Google Books API 作為資料來源，提供書籍資訊的查詢功能。"
            },
            new() {
                Icon = "bi bi-info-square",
                Title = "顯示資料",
                Description = "書籍封面、書名、作者、出版社、書籍簡介、出版日期、ISBN-10、ISBN-13"
            },
            new() {
                Icon = "bi bi-upc",
                Title = "ISBN 查詢功能",
                Description = "在搜尋欄位中輸入書籍的 ISBN，系統會依據輸入的 ISBN 查詢對應的書籍資料。"
            },
            new() {
                Icon = "bi bi-exclamation-lg",
                Title = "查無資料",
                Description = "ISBN 輸入錯誤，或資料源沒有提供書籍資料"
            }
        ];

        return View(guidelineItems);
    }

    /// <summary>
    /// Displays the book search view.
    /// </summary>
    /// <returns>A <see cref="IActionResult"/> that renders the book search page.</returns>
    public IActionResult BookSearch()
    {
        return View();
    }

    /// <summary>
    /// Displays the version view.
    /// </summary>
    /// <returns>The view for the version information.</returns>
    public IActionResult Version()
    {
        List<VersionItemVm> versions = [
            new() {
                Version = "1.0.0",
                Description = "Initial release - Supports searching book information by ISBN.",
                ReleasedDate = new DateOnly(2026, 5, 1)
            }
        ];

        return View(versions);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
