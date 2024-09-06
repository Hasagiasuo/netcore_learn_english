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
        UpdateType.CallbackQuery => BotCallbackQuery(update.CallbackQuery),
        _ => BotUnknownCallback(update)
      };
      try {
        await handler
      } catch (Exception ex) {
        await HandleErrorAsync(ex);
      }
    }
    private async object BotReceived(Message message) {
      await _bot_client.SendTextMessageAsync(message.Chat.Id, message.Text);
    }
    private async Task BotCallbackQuery(CallbackQuery query) {
      _bot_client.SendTextMessageAsync(query.Message.Chat.Id, $"{query.Data}");
    }
    private object BotUnknownCallback(Update update) {
      _logger.LogInformation($"Undefined types: {update.Type}!");
      return Task.CompletedTask;
    }
    public Task HandleErrorAsync(Exception ex) {
      _logger.LogInformation($"Error: {ex.ToString()}");
      return Task.CompletedTask;
    }
  }
}