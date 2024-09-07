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
    private readonly string _assetsFolderPath = "assets"; 
    private readonly string _statFilePath = "stat/stat.txt";

    public IActionResult Stats()
    {
        
=======
    private readonly string _assetsFolderPath = "assets"; // ���� �� ����� assets
    private readonly string _statFilePath = "stat/stat.txt"; // ���� �� stat.txt

    public IActionResult Stats()
    {
        // ϳ��������� ������� ����� � ��� ������ ����� assets
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
        var totalWords = Directory.GetFiles(_assetsFolderPath, "*.txt")
                                  .SelectMany(file => System.IO.File.ReadAllLines(file))
                                  .Where(line => !string.IsNullOrWhiteSpace(line))
                                  .Count();

<<<<<<< HEAD
        
=======
        // ������ ������� �������� ��� � stat.txt
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
        var learnedWords = System.IO.File.ReadAllLines(_statFilePath)
                                         .Where(line => !string.IsNullOrWhiteSpace(line))
                                         .Count();

<<<<<<< HEAD
        
        int learnedPercentage = (totalWords > 0) ? (learnedWords * 100) / totalWords : 0;
        int remainingPercentage = 100 - learnedPercentage;

        
        ViewBag.LearnedPercentage = learnedPercentage;
        ViewBag.RemainingPercentage = remainingPercentage;
        ViewBag.TotalWords = totalWords; 
=======
        // ���������� �������
        int learnedPercentage = (totalWords > 0) ? (learnedWords * 100) / totalWords : 0;
        int remainingPercentage = 100 - learnedPercentage;

        // �������� ���� �� View
        ViewBag.LearnedPercentage = learnedPercentage;
        ViewBag.RemainingPercentage = remainingPercentage;
        ViewBag.TotalWords = totalWords; // ����� �������� �������� ������� ��� �����������
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc

        return View("Stats");
    }

    
    [HttpPost]
    public IActionResult ResetDatabase()
    {
        System.IO.File.WriteAllText(_statFilePath, string.Empty); 
        return RedirectToAction("Stats"); 
    }
    private readonly string _basePath = "assets/";
    private readonly string _basePathStat = "stat/";

<<<<<<< HEAD
    
=======
    // ����� ��� ��������� ����� Animals
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
    public IActionResult Animals()
    {
        var data = ReadFile("animals.txt");
        return View("Animals", data);
    }

<<<<<<< HEAD
    
=======
    // ����� ��� ��������� ������ � Animals
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
    [HttpPost]
    public IActionResult AddAnimal(string newAnimal)
    {
        if (!string.IsNullOrEmpty(newAnimal))
        {
            AppendToFile("animals.txt", newAnimal);
        }
        return RedirectToAction("Animals");
    }

<<<<<<< HEAD
    
=======
    // ����� ��� ��������� ������ � Animals
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
    [HttpPost]
    public IActionResult DeleteAnimal(string animalToDelete)
    {
        if (!string.IsNullOrEmpty(animalToDelete))
        {
            DeleteFromFile("animals.txt", animalToDelete);
        }
        return RedirectToAction("Animals");
    }

<<<<<<< HEAD
    
=======
    // ������ ��� ����� �������� (Colors, Fruits, Weather)
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
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

<<<<<<< HEAD
   
=======
    // ������� �����
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
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

<<<<<<< HEAD
    
=======
    // ��������� � ����
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
    private void AppendToFile(string fileName, string content)
    {
        var filePath = Path.Combine(_basePath, fileName);
        System.IO.File.AppendAllText(filePath,  content + "\n", Encoding.UTF8);
    }

<<<<<<< HEAD
    
=======
    // ��������� � �����
>>>>>>> 2a1eeeb3aac87e8cc804c415ec364b9040648adc
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
        return View("Game", "apppple");
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
