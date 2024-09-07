using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.IO;

namespace Telebot {
  public class TelebotMN() {
    private static List<List<string>> _currentData;
    private static StreamReader rd;
    private static TelegramBotClient _bot;
    public async static Task Main() {
      _bot =  new TelegramBotClient("7505959570:AAEa2MtBT3f579qcsyqOTQcQ2AsYK5TqCGg");
      await _bot.DeleteWebhookAsync();
      _bot.StartReceiving(EventHandler, ErrorHandler);
      Console.WriteLine("INFO: Bot starting..\nPress any key for closing");
      Console.ReadKey();
      Console.WriteLine("INFO: Bot stoped!");
    }
    public static Task EventHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
      if (update.Type == UpdateType.Message && update.Message?.Text != null) {
        switch (update.Message.Text) {
        case "/start":
          WelcomeMessage(update);
          SendCategorySelection(update.Message.Chat.Id, null);
          break;
        }
      } else if (update.Type == UpdateType.CallbackQuery) {
        var callbackData = update.CallbackQuery.Data;
        switch (callbackData) {
        case "animals":
          HandleAnimalsCategory(update.CallbackQuery);
          break;
        case "colors":
          HandleColorsCategory(update.CallbackQuery);
          break;
        case "fruits":
          HandleFruitsCategory(update.CallbackQuery);
          break;
        case "weather":
          HandleWeatherCategory(update.CallbackQuery);
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
    private static async void SendCategorySelection(long chatId, string? add_text)
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
      if(add_text != null)
        await _bot.SendTextMessageAsync(chatId, $"{add_text}\nОберіть категорію для навчання \U0001F920", replyMarkup: classWordsKeyboard);
      else 
        await _bot.SendTextMessageAsync(chatId, "Оберіть категорію для навчання \U0001F920", replyMarkup: classWordsKeyboard);
    }
    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken) {
      Console.WriteLine($"ERROR: {error.ToString()}");
      return Task.CompletedTask;
    }
    private static Task WelcomeMessage(Update update) {
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
        "'Magic Words Adventure' – це не просто гра, а справжня магічна подорож у світ англійських слів! Вивчайте англійську легко та весело! \U0001F308 \u2728"
      );
      return Task.CompletedTask;
    }
    private static async void HandleAnimalsCategory(CallbackQuery callbackQuery) {
      await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ви вибрали категорію: Тварини 🐶");
      rd = new StreamReader("../web/assets/animals.txt");
      sendNext(callbackQuery);
    }
    private static async void HandleColorsCategory(CallbackQuery callbackQuery) {
      await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ви вибрали категорію: Кольори 🌈");
      rd = new StreamReader("../web/assets/colors.txt");
      sendNext(callbackQuery);
    }
    private static async void HandleFruitsCategory(CallbackQuery callbackQuery) {
      await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ви вибрали категорію: Фрукти 🍎");
      rd = new StreamReader("../web/assets/fruits.txt");
      sendNext(callbackQuery);
    }
    private static async void HandleWeatherCategory(CallbackQuery callbackQuery) {
      await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ви вибрали категорію: Погода ☀️");
      rd = new StreamReader("../web/assets/weather.txt");
      sendNext(callbackQuery);
    }
    private static async void HandleNext(CallbackQuery callbackQuery) {
      sendNext(callbackQuery);
    }
    private static InlineKeyboardMarkup gen_keyboard() {
      InlineKeyboardMarkup tmp_k = new InlineKeyboardMarkup(
        new InlineKeyboardButton[][]
        {
          new InlineKeyboardButton[]
          {
            InlineKeyboardButton.WithUrl("Практика", "https://79fe-77-47-238-26.ngrok-free.app/home/Game"),
            InlineKeyboardButton.WithCallbackData("Наступне", "next")
          },
          new InlineKeyboardButton[] 
          {
            InlineKeyboardButton.WithCallbackData("Змінити категорію", "edit")
          }
        }
      );
      return tmp_k;
    }
    private static async void sendNext(CallbackQuery callbackQuery) {
      string line = await rd.ReadLineAsync() ?? "";
      if(line != "") {
        string[] de_line = line.Split("|");
        using(var sr = new StreamReader("../web/stat/stat.txt")) {
          string a_stat = await sr.ReadToEndAsync();
          foreach(string l in a_stat.Split("\n")) {
            if(l.Split("|")[0] == de_line[0]) sendNext(callbackQuery);
          }
        }
        using(var sw = new StreamWriter("../web/stat/stat.txt", true)) { await sw.WriteLineAsync(de_line[0]); }
        await using Stream stream = System.IO.File.OpenRead($"../web/img/{de_line[0].ToLower()}.jfif");
        await _bot.SendPhotoAsync(
          callbackQuery.Message.Chat.Id, 
          InputFile.FromStream(stream), 
          caption: $"{de_line[0]}\n{de_line[1]}", 
          replyMarkup: gen_keyboard());
      } else {
        SendCategorySelection(callbackQuery.Message.Chat.Id, $"Схоже ви вивчили всі можливі слова цієї категорії \U0001F914");
      }
    }
  }
}
