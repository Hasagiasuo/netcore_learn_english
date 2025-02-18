using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using web.Models;
using web.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace web.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> ResetDatabase()
    {
        _context.Words.RemoveRange(_context.Words);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> AddWord(string newWord, string ukrainianWord, string category, IFormFile image)
    {
        if (string.IsNullOrWhiteSpace(newWord) || string.IsNullOrWhiteSpace(ukrainianWord) || image == null)
        {
            ModelState.AddModelError("", "Усі поля обов'язкові.");
            return View();
        }

        byte[] imageData;
        using (var memoryStream = new MemoryStream())
        {
            await image.CopyToAsync(memoryStream);
            imageData = memoryStream.ToArray();
        }

        var word = new Word
        {
            English = newWord,
            Ukrainian = ukrainianWord,
            Category = category,
            Image = imageData
        };

        _context.Words.Add(word);
        await _context.SaveChangesAsync();


        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteWord(string word)
    {
        var wordToDelete = await _context.Words
            .FirstOrDefaultAsync(w => w.English == word);

        if (wordToDelete != null)
        {
            _context.Words.Remove(wordToDelete);
            await _context.SaveChangesAsync();

        }

        return RedirectToAction("Stats");
    }
    public async Task<IActionResult> GetWordsByCategory(string category)
    {
        var words = await _context.Words
            .Where(w => w.Category == category)
            .ToListAsync();

        foreach (var word in words)
        {
            if (word.Image != null)
            {
                var base64Image = $"data:image/png;base64,{Convert.ToBase64String(word.Image)}";
                ViewData[$"Image_{word.Id}"] = base64Image;
            }
            else
            {
                ViewData[$"Image_{word.Id}"] = null;
            }
        }

        ViewData["Category"] = category;

        return View(words);
    }

    [HttpPost]
    public async Task<IActionResult> AddWordToCategory(string newWord, string category)
    {
        if (!string.IsNullOrEmpty(newWord))
        {
            var word = new Word
            {
                English = newWord,
                Category = category,
                Ukrainian = "Переклад не вказано"
            };

            _context.Words.Add(word);
            await _context.SaveChangesAsync();

        }

        return RedirectToAction("GetWordsByCategory", new { category });
    }
    [HttpPost]
    public async Task<IActionResult> DeleteWordFromCategory(string word, string category)
    {
        var wordToDelete = await _context.Words
            .FirstOrDefaultAsync(w => w.English == word && w.Category == category);

        if (wordToDelete != null)
        {
            _context.Words.Remove(wordToDelete);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("GetWordsByCategory", new { category });
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file, int wordId)
    {
        if (file != null && file.Length > 0)
        {
            var allowedExtensions = new[] { ".jfif", ".jpg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
            {
                ViewBag.Message = "Invalid file type!";
                return View("Index");
            }

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var word = await _context.Words.FindAsync(wordId);

                if (word != null)
                {
                    word.Image = memoryStream.ToArray();
                    await _context.SaveChangesAsync();
                }
            }

            ViewBag.Message = "File uploaded successfully!";
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
    public IActionResult AddWord()
    {
        return View();
    }
    // Сторінка помилки
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}