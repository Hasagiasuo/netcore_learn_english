using hackador4.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace hackador4.Controllers {
  public class WebhookController {
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] HandleUpdate handleUpdate, [FromBody] Update update) {
      await handleUpdate.EchoHandle(update);
      return Ok();
    }
  }
}