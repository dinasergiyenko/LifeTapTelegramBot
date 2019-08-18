using System.Threading.Tasks;
using LifeTapTelegramBot.Bot;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace LifeTapTelegramBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IBotService _botService;

        public MessageController(
            IBotService botService
        )
        {
            _botService = botService;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello";
        }

        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            if (update == null)
            {
                return Ok();
            }

            var botClient = await _botService.GetBotClientAsync();
            var commands = _botService.GetCommands();
            var isCommandParsed = false;

            foreach (var command in commands)
            {
                var shouldBeExecuted = command.ShouldBeExecuted(update.Message);

                if (shouldBeExecuted.Key)
                {
                    isCommandParsed = true;
                    await command.ExecuteAsync(update.Message, botClient);
                }
                else if (!shouldBeExecuted.Key && !string.IsNullOrEmpty(shouldBeExecuted.Value))
                {
                    isCommandParsed = true;
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, shouldBeExecuted.Value);
                }
            }

            if (!isCommandParsed)
            {
                await botClient.SendTextMessageAsync(update.Message.Chat.Id, "Sorry, I can't understand your command :(");
            }

            return Ok();
        }

    }
}