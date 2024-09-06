using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace hackador4.Services {
  public class HandleUpdate {
    private readonly ILogger<HandleUpdate> _logger;
    private readonly ITelegramBotClient _bot_client;
    public HandleUpdate(ILogger<HandleUpdate> logger, ITelegramBotClient bot_client) {
      _logger = logger;
      _bot_client = bot_client;
    }
    public async Task EchoHandle(Update update) {
      var handler = update.Type switch {
        UpdateType.Message => BotReceived(update.Message),
        UpdateType.CallbackQuery => BotReceived(update.CallbackQuery),
        _ => BotUnknownCallback(update)
      };
    }
    private async object BotReceived(Message message) {
      await _bot_client.SendTextMessageAsync(message.Chat.Id, )
    }
    private object BotCallbackQuery(CallbackQuery query) {

    }
    private object BotUnknownCallback(Update update) {

    }
  }
}