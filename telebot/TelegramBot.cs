using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public class TelegramBot
{
    private readonly TelegramBotClient _botClient;

    public TelegramBot(string token)
    {
        _botClient = new TelegramBotClient(token);
    }

    public async Task HandleUpdateAsync(Update update)
    {
        if (update.Type == UpdateType.Message && update.Message.Type == MessageType.Text)
        {
            var messageText = update.Message.Text;
            var chatId = update.Message.Chat.Id;

            if (messageText == "/start")
            {
                await _botClient.SendTextMessageAsync(chatId, "Welcome to the bot!");
            }
            else
            {
                await _botClient.SendTextMessageAsync(chatId, $"You said: {messageText}");
            }
        }
    }
}
