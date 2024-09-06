using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
namespace hackador4.Services {
  public class WebhookConfig : IHostedService
  {
    private readonly ILogger<WebhookConfig> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;

    public WebhookConfig(ILogger<WebhookConfig> logger, IServiceProvider serviceProvider, IConfiguration configuration) {
      _logger = logger;
      _serviceProvider = serviceProvider;
      _configuration = configuration.GetSection("TelegramBot").Get<hackador4.Models.TelebotConfig>();
    }
    public async Task StartAsync(CancellationToken cancellationToken) {
      using var scope = _serviceProvider.CreateScope();
      var bot_client = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
      var webhook_url = $@"{_configuration.Host}/bot/{_configuration.Token}";
      _logger.LogInformation("Setting webhook!");
      await bot_client.SetWebhookAsync(
        url: webhook_url,
        allowedUpdates: Array.Empty<UpdateType>(),
        cancellationToken: cancellationToken
      );
      await bot_client.SendTextMessageAsync(580819271, "Bot welcome!");
    }

    public async Task StopAsync(CancellationToken cancellationToken) {
      using var scope = _serviceProvider.CreateScope();
      var bot_client = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
      _logger.LogInformation("Removing webhook!");
      await bot_client.SendTextMessageAsync(580819271, "Bot removing!");
    }
  }
}