using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace Telebot {
  public class TelebotMN() {
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
          SendCategorySelection(update.Message.Chat.Id);
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
        }
      }
      return Task.CompletedTask;
    }
    private static async void SendCategorySelection(long chatId)
    {
      InlineKeyboardMarkup classWordsKeyboard = new InlineKeyboardMarkup(
        new InlineKeyboardButton[][]
        {
          new InlineKeyboardButton[]
          {
            InlineKeyboardButton.WithCallbackData("Тварини", "animals"),
            InlineKeyboardButton.WithCallbackData("Кольри", "colors")
          },
          new InlineKeyboardButton[]
          {
            InlineKeyboardButton.WithCallbackData("Фрукти", "fruits"),
            InlineKeyboardButton.WithCallbackData("Погода", "weather")
          }
        }
      );

      await _bot.SendTextMessageAsync(chatId, "Оберіть категорію для навчання", replyMarkup: classWordsKeyboard);
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
    }
    private static async void HandleColorsCategory(CallbackQuery callbackQuery) {
      await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ви вибрали категорію: Кольори 🌈");
        }
    private static async void HandleFruitsCategory(CallbackQuery callbackQuery) {
      await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ви вибрали категорію: Фрукти 🍎");
        }
    private static async void HandleWeatherCategory(CallbackQuery callbackQuery) {
      await _bot.SendTextMessageAsync(callbackQuery.Message.Chat.Id, "Ви вибрали категорію: Погода ☀️");
    }
  }
}