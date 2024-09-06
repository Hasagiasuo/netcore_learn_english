using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telebot {
  public class TelebotMN() {
    private static TelegramBotClient _bot;
    public async static Task Main() {
      _bot =  new TelegramBotClient("7505959570:AAEa2MtBT3f579qcsyqOTQcQ2AsYK5TqCGg");
      await _bot.DeleteWebhookAsync();
      _bot.StartReceiving(EventHandler, ErrorHandler);
      Console.WriteLine("INFO: Bot starting..\nPress any key for closing");
      Console.ReadKey();
    }
    public static Task EventHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) {
      switch(update.Message.Text) {
        case "/start":
          WelcomeMessage(update);
          break;
        default:
         break;
      }
      return Task.CompletedTask;
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
  }
}