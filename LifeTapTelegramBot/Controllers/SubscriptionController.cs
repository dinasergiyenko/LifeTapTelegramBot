using System.Threading.Tasks;
using LifeTapTelegramBot.Bot;
using LifeTapTelegramBot.BusinessLogicLayer.Interfaces;
using LifeTapTelegramBot.Common;
using LifeTapTelegramBot.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LifeTapTelegramBot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly IBotService _botService;
        private readonly IUserService _userService;
        private readonly BotMessages _botMessages;

        public SubscriptionController(
            IBotService botService,
            IUserService userService,
            IOptions<BotMessages> botMessages)
        {
            _botService = botService;
            _userService = userService;
            _botMessages = botMessages.Value;
        }

        [HttpPost]
        public async Task<OkResult> Post([FromBody] EventViewModel eventViewModel)
        {
            var botClient = await _botService.GetBotClientAsync();
            var subscribedUsers = _userService.GetSubscribers(eventViewModel.Usernames);
            var message = string.Format(
                _botMessages.SubscriptionMessage,
                eventViewModel.Title,
                eventViewModel.Description);

            foreach (var subscribedUser in subscribedUsers)
            {
                await botClient.SendTextMessageAsync(subscribedUser.ChatId, message);
            }

            return Ok();
        }
    }
}