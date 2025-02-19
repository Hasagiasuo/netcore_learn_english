using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using Microsoft.EntityFrameworkCore;
using web.Models;
using web.Data;
using System.Linq;

namespace Telebot
{
    public class TelebotMN
    {
        private static List<Word> _currentWords;
        private static int _currentIndex;
        private static TelegramBotClient _bot;
        private static ApplicationDbContext _dbContext;

        public async static Task Main()
        {
            _bot = new TelegramBotClient("5392641716:AAGhSt6l2m5Jyz8fITISVGPv8TeVEyQ1Vwg");
            await _bot.DeleteWebhookAsync();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("connection string");
            _dbContext = new ApplicationDbContext(optionsBuilder.Options);

            _bot.StartReceiving(EventHandler, ErrorHandler);
            Console.WriteLine("INFO: Bot starting..\nPress any key for closing");
            Console.ReadKey();
            Console.WriteLine("INFO: Bot stopped!");
        }

        public static Task EventHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                switch (update.Message.Text)
                {
                    case "/start":
                        WelcomeMessage(update);
                        SendCategorySelection(update.Message.Chat.Id, null);
                        break;
                }
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                var callbackData = update.CallbackQuery.Data;
                switch (callbackData)
                {
                    case "animals":
                        HandleCategorySelection(update.CallbackQuery, "Тварини");
                        break;
                    case "colors":
                        HandleCategorySelection(update.CallbackQuery, "Кольори");
                        break;
                    case "fruits":
                        HandleCategorySelection(update.CallbackQuery, "Фрукти");
                        break;
                    case "weather":
                        HandleCategorySelection(update.CallbackQuery, "Погода");
                        break;
                    case "next":
                        HandleNext(update.CallbackQuery);
                        break;
                    case "edit":
                        SendCategorySelection(update.CallbackQuery.Message.Chat.Id, null);
                        break;
                }
            }
            return Task.CompletedTask;
        }

        private static async void SendCategorySelection(long chatId, string? addText)
        {
            InlineKeyboardMarkup classWordsKeyboard = new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Тварини", "animals"),
                        InlineKeyboardButton.WithCallbackData("Кольори", "colors")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Фрукти", "fruits"),
                        InlineKeyboardButton.WithCallbackData("Погода", "weather")
                    }
                }
            );

            if (addText != null)
                await _bot.SendTextMessageAsync(chatId, $"{addText}\nОберіть категорію для навчання \U0001F920", replyMarkup: classWordsKeyboard);
            else
                await _bot.SendTextMessageAsync(chatId, "Оберіть категорію для навчання \U0001F920", replyMarkup: classWordsKeyboard);
        }

        private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            Console.WriteLine($"ERROR: {error.ToString()}");
            return Task.CompletedTask;
        }

        private static Task WelcomeMessage(Update update)
        {
            _bot.SendTextMessageAsync(update.Message.Chat.Id,
                "'Magic Words Adventure' – Весела гра для вивчення англійської мови!\nВітаємо маленьких чарівників у світі 'Magic Words Adventure' ! \U0001F31F Готові вирушити у подорож через магічні королівства та допомогти героям розгадати слова на англійській мові? Тут кожне слово — це чарівне заклинання, яке відчиняє двері у нові захоплюючі світи!");
            _bot.SendTextMessageAsync(update.Message.Chat.Id,
                "Подорожуйте через рівні, кожен з яких представляє нову категорію слів: від тварин і фруктів до транспорту і кольорів.\n" +
                "У грі 'Magic Words Adventurer' ваш герой стоїть посередині та ловить літери, які падають зверху. Завдання – зібрати всі правильні літери для складання слова. " +
                "Якщо ви ловите неправильну літеру, втрачаєте життя! \u2764\uFE0F\n\n" +
                "\U0001F3AE Геймплей:\n" +
                "Ловіть літери з двох боків, щоб скласти слово.\n" +
                "Уникайте неправильних літер, інакше втратите життя (всього 3 спроби).\n" +
                "Чи зможете ви зібрати всі слова та не втратити життя? \U0001F31F\n\n" +
                "'Magic Words Adventure' – це не просто гра, а справжня магічна подорож у світ англійських слів! Вивчайте англійську легко та весело! \U0001F308 \u2728" +
                "Для керування прогресу перейдіть по посиланню: https://f15d-46-211-84-138.ngrok-free.app"
            );
            return Task.CompletedTask;
        }

        private static async void HandleCategorySelection(CallbackQuery callbackQuery, string category)
        {
            await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, $"Ви вибрали категорію: {category}");

            _currentWords = await _dbContext.Words
                .Where(w => w.Category == category)
                .ToListAsync();

            _currentIndex = 0;

            if (_currentWords.Any())
            {
                await SendNextWord(callbackQuery);
            }
            else
            {
                await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Слова для цієї категорії відсутні.");
            }
        }

        private static async void HandleNext(CallbackQuery callbackQuery)
        {
            if (_currentWords == null || !_currentWords.Any())
            {
                await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Слова для цієї категорії відсутні.");
                return;
            }

            _currentIndex++;

            if (_currentIndex >= _currentWords.Count)
            {
                _currentIndex = 0;
            }

            await SendNextWord(callbackQuery);
        }

        private static InlineKeyboardMarkup GenKeyboard()
        {
            return new InlineKeyboardMarkup(
                new InlineKeyboardButton[][]
                {
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithUrl("Практика", "https://f15d-46-211-84-138.ngrok-free.app/home/Game"),
                        InlineKeyboardButton.WithCallbackData("Наступне", "next")
                    },
                    new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData("Змінити категорію", "edit")
                    }
                }
            );
        }

        private static async Task SendNextWord(CallbackQuery callbackQuery)
        {
            var word = _currentWords[_currentIndex];

            if (word.Image != null)
            {
                await using var stream = new MemoryStream(word.Image);
                await _bot.SendPhotoAsync(
                    callbackQuery.Message.Chat.Id,
                    InputFile.FromStream(stream),
                    caption: $"{word.English}\n{word.Ukrainian}",
                    replyMarkup: GenKeyboard());
            }
            else
            {
                await _bot.SendTextMessageAsync(
                    callbackQuery.Message.Chat.Id,
                    $"Зображення відсутнє \U0001F915\n{word.English}\n{word.Ukrainian}",
                    replyMarkup: GenKeyboard());
            }
        }
    }
}