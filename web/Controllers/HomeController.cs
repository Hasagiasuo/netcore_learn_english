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
<<<<<<< HEAD

    private readonly string _assetsFolderPath = "assets"; // Шлях до папки assets
    private readonly string _statFilePath = "stat/stat.txt"; // Шлях до stat.txt

    public IActionResult Stats()
    {
        // Підраховуємо кількість рядків у всіх файлах папки assets
        var totalWords = Directory.GetFiles(_assetsFolderPath, "*.txt")
                                  .SelectMany(file => System.IO.File.ReadAllLines(file))
                                  .Where(line => !string.IsNullOrWhiteSpace(line))
                                  .Count();

        // Читаємо кількість вивчених слів зі stat.txt
        var learnedWords = System.IO.File.ReadAllLines(_statFilePath)
                                         .Where(line => !string.IsNullOrWhiteSpace(line))
                                         .Count();

        // Розрахунок відсотків
        int learnedPercentage = (totalWords > 0) ? (learnedWords * 100) / totalWords : 0;
        int remainingPercentage = 100 - learnedPercentage;

        // Передаємо дані до View
        ViewBag.LearnedPercentage = learnedPercentage;
        ViewBag.RemainingPercentage = remainingPercentage;
        ViewBag.TotalWords = totalWords; // Можна передати загальну кількість для відображення

        return View("Stats");
    }
=======
>>>>>>> 831954173e2f7625e8e017d40708f5ce2854a136
    private readonly string _basePath = "assets/";
    private readonly string _basePathStat = "stat/";

    // Метод для перегляду файлу Animals
    public IActionResult Animals()
    {
        var data = ReadFile("animals.txt");
        return View("Animals", data);
    }

    // Метод для додавання запису в Animals
    [HttpPost]
    public IActionResult AddAnimal(string newAnimal)
    {
        if (!string.IsNullOrEmpty(newAnimal))
        {
            AppendToFile("animals.txt", newAnimal);
        }
        return RedirectToAction("Animals");
    }

    // Метод для видалення запису з Animals
    [HttpPost]
    public IActionResult DeleteAnimal(string animalToDelete)
    {
        if (!string.IsNullOrEmpty(animalToDelete))
        {
            DeleteFromFile("animals.txt", animalToDelete);
        }
        return RedirectToAction("Animals");
    }

    // Методи для інших категорій (Colors, Fruits, Weather)
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

    public IActionResult Stats()
    {
        var data = ReadStat("stat.txt");
        return View("Weather", data);
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

    // Читання файлу
    private string[] ReadFile(string fileName)
    {
        var filePath = Path.Combine(_basePath, fileName);
        return System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
    }


    private string[] ReadStat(string fileName)
    {
        var filePath = Path.Combine(_basePathStat, fileName);
        return System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
    }

    // Додавання в файл
    private void AppendToFile(string fileName, string content)
    {
        var filePath = Path.Combine(_basePath, fileName);
        System.IO.File.AppendAllText(filePath,  content + "\n", Encoding.UTF8);
    }

    // Видалення з файлу
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

    public IActionResult Game()
    {
        return View();
    }



    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
