using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly string _basePath = "assets/";

    // ����� ��� ��������� ����� Animals
    public IActionResult Animals()
    {
        var data = ReadFile("animals.txt");
        return View("Animals", data);
    }

    // ����� ��� ��������� ������ � Animals
    [HttpPost]
    public IActionResult AddAnimal(string newAnimal)
    {
        if (!string.IsNullOrEmpty(newAnimal))
        {
            AppendToFile("animals.txt", newAnimal);
        }
        return RedirectToAction("Animals");
    }

    // ����� ��� ��������� ������ � Animals
    [HttpPost]
    public IActionResult DeleteAnimal(string animalToDelete)
    {
        if (!string.IsNullOrEmpty(animalToDelete))
        {
            DeleteFromFile("animals.txt", animalToDelete);
        }
        return RedirectToAction("Animals");
    }

    // ������ ��� ����� �������� (Colors, Fruits, Weather)
    public IActionResult Colors()
    {
        var data = ReadFile("colors.txt");
        return View("Colors", data);
    }

    [HttpPost]
    public IActionResult AddColor(string newColor)
    {
        if (!string.IsNullOrEmpty(newColor))
        {
            AppendToFile("colors.txt", newColor);
        }
        return RedirectToAction("Colors");
    }

    [HttpPost]
    public IActionResult DeleteColor(string colorToDelete)
    {
        if (!string.IsNullOrEmpty(colorToDelete))
        {
            DeleteFromFile("colors.txt", colorToDelete);
        }
        return RedirectToAction("Colors");
    }

    public IActionResult Fruits()
    {
        var data = ReadFile("fruits.txt");
        return View("Fruits", data);
    }

    [HttpPost]
    public IActionResult AddFruit(string newFruit)
    {
        if (!string.IsNullOrEmpty(newFruit))
        {
            AppendToFile("fruits.txt", newFruit);
        }
        return RedirectToAction("Fruits");
    }

    [HttpPost]
    public IActionResult DeleteFruit(string fruitToDelete)
    {
        if (!string.IsNullOrEmpty(fruitToDelete))
        {
            DeleteFromFile("fruits.txt", fruitToDelete);
        }
        return RedirectToAction("Fruits");
    }

    public IActionResult Weather()
    {
        var data = ReadFile("weather.txt");
        return View("Weather", data);
    }

    [HttpPost]
    public IActionResult AddWeather(string newWeather)
    {
        if (!string.IsNullOrEmpty(newWeather))
        {
            AppendToFile("weather.txt", newWeather);
        }
        return RedirectToAction("Weather");
    }

    [HttpPost]
    public IActionResult DeleteWeather(string weatherToDelete)
    {
        if (!string.IsNullOrEmpty(weatherToDelete))
        {
            DeleteFromFile("weather.txt", weatherToDelete);
        }
        return RedirectToAction("Weather");
    }

    // ������� �����
    private string[] ReadFile(string fileName)
    {
        var filePath = Path.Combine(_basePath, fileName);
        return System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
    }

    // ��������� � ����
    private void AppendToFile(string fileName, string content)
    {
        var filePath = Path.Combine(_basePath, fileName);
        System.IO.File.AppendAllText(filePath, content + "\n", Encoding.UTF8);
    }

    // ��������� � �����
    private void DeleteFromFile(string fileName, string content)
    {
        var filePath = Path.Combine(_basePath, fileName);
        var lines = System.IO.File.ReadAllLines(filePath).ToList();
        lines.Remove(content);
        System.IO.File.WriteAllLines(filePath, lines, Encoding.UTF8);
    }

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    public IActionResult Index()
    {
        return View();
    }

<<<<<<< HEAD
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

=======
>>>>>>> 3641aff8edc5592d3aa257981f1726ca350b2089
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
