using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LifeTapTelegramBot.BusinessLogicLayer.Interfaces;
using LifeTapTelegramBot.Common;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace LifeTapTelegramBot.Commands
{
    public class StartCommand : CommandBase
    {
        private readonly IUserService _userService;
        private readonly BotMessages _commandMessages;

        public override string Name  => "/start";

        public StartCommand(
            IUserService userService,
            IOptions<BotMessages> commandMessages
        )
        {
            _userService = userService;
            _commandMessages = commandMessages.Value;
        }

        public override async Task ExecuteAsync(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            var parts = GetCommandParts(message.Text);

            _userService.Add(message.From.Id, chatId, parts[1]);

            await botClient.SendTextMessageAsync(chatId, string.Format(_commandMessages.StartCommandSuccessMessage, message.From.FirstName));
        }

        public override KeyValuePair<bool,string> ShouldBeExecuted(Message message)
        {
            var parts = GetCommandParts(message.Text);
            var isStartCommand = message.Text.Contains(Name);

            if (!isStartCommand)
            {
                return ReturnValidationAnswer(false);
            }

            if (parts.Length == 1)
            {
                return ReturnValidationAnswer(false, _commandMessages.StartCommandErrorMessage);
            }

            return ReturnValidationAnswer(true);
        }

        private string[] GetCommandParts(string text)
        {
            return text.Split(' ');
        }
    }
}