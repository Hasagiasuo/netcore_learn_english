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

    private readonly string _assetsFolderPath = "assets"; 
    private readonly string _statFilePath = "stat/stat.txt";
    private readonly string _logFilePath = "stat/stat.txt";   

    public IActionResult Stats()
    {
        
        var learnedWords = System.IO.File.ReadAllLines(_statFilePath)
                                         .Where(line => !string.IsNullOrWhiteSpace(line))
                                         .Count();

       
        var lastActions = System.IO.File.ReadAllLines(_logFilePath)
                                        .Where(line => !string.IsNullOrWhiteSpace(line))
                                        .TakeLast(5)
                                        .ToList(); 

        
        int learnedPercentage = (learnedWords * 100) / 20;  

        
        ViewBag.LearnedPercentage = learnedPercentage;
        ViewBag.RemainingPercentage = 100 - learnedPercentage;
        ViewBag.LastActions = lastActions;

        return View();
    }

    
    private void LogAction(string action)
    {
        
        string logMessage = $"{DateTime.Now}: {action}";
        System.IO.File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
    }

    
    [HttpPost]
    public IActionResult ResetDatabase()
    {
        System.IO.File.WriteAllText(_statFilePath, string.Empty); 

       
        LogAction("Database reset.");

        return RedirectToAction("Stats");
    }

    
    [HttpPost]
    public IActionResult AddWord(string newWord)
    {
        System.IO.File.AppendAllText(_statFilePath, newWord + Environment.NewLine); 

        
        LogAction($"Added word: {newWord}");

        return RedirectToAction("Stats");
    }

    [HttpPost]
    public IActionResult DeleteWord(string word)
    {
        var allWords = System.IO.File.ReadAllLines(_statFilePath).ToList();
        allWords.Remove(word); 
        System.IO.File.WriteAllLines(_statFilePath, allWords); 

       
        LogAction($"Deleted word: {word}");

        return RedirectToAction("Stats");
    }


 
    private readonly string _basePath = "assets/";
    private readonly string _basePathStat = "stat/";

    
    public IActionResult Animals()
    {
        var data = ReadFile("animals.txt");
        return View("Animals", data);
    }

    
    [HttpPost]
    public IActionResult AddAnimal(string newAnimal)
    {
        if (!string.IsNullOrEmpty(newAnimal))
        {
            AppendToFile("animals.txt", newAnimal);
        }
        return RedirectToAction("Animals");
    }

    
    [HttpPost]
    public IActionResult DeleteAnimal(string animalToDelete)
    {
        if (!string.IsNullOrEmpty(animalToDelete))
        {
            DeleteFromFile("animals.txt", animalToDelete);
        }
        return RedirectToAction("Animals");
    }

    
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

    
    private void AppendToFile(string fileName, string content)
    {
        var filePath = Path.Combine(_basePath, fileName);
        System.IO.File.AppendAllText(filePath,  content + "\n", Encoding.UTF8);
    }

    
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
// /Users/dmitro/data/Code/hackador4/web/curr/current_word.txt
    public IActionResult Game()
    {
        string target;
        using(var wr = new StreamReader("./curr/current_word.txt")) {
            target = wr.ReadLine() ?? "DOTNET";
        }
        return View("Game", target);
    }


    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            // Validate file type
            var allowedExtensions = new[] { ".jfif"};
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ViewBag.Message = "Invalid file type!";
                return View("Index");
            }

            // Define the file path
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "img", file.FileName);

            try
            {
                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                ViewBag.Message = "File uploaded successfully!";
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ViewBag.Message = $"File upload failed: {ex.Message}";
            }
        }
        else
        {
            ViewBag.Message = "No file selected!";
        }

        return View("Index");
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
