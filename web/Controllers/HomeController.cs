using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Colors()
    {
        // Path to the text file (adjust the path as needed)
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "worlds/colors.txt");

        // Read the file asynchronously
        var fileContent = await System.IO.File.ReadAllTextAsync(filePath);

        // Pass the content to the view
        return View("Colors", fileContent);
        //return View();
    }

    public IActionResult Weather()
    {
        return View();
    }

    public IActionResult Fruits()
    {
        return View();
    }

    public IActionResult Animals()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
