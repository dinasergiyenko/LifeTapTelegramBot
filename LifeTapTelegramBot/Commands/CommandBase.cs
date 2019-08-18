using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace LifeTapTelegramBot.Commands
{
    public abstract class CommandBase
    {
        public abstract string Name { get; }

        public abstract Task ExecuteAsync(Message message, TelegramBotClient botClient);

        public virtual KeyValuePair<bool, string> ShouldBeExecuted(Message message)
        {
            if (message.Type != MessageType.Text)
            {
                return ReturnValidationAnswer(false);
            }

            return ReturnValidationAnswer(message.Text.Contains(Name));
        }

        protected virtual KeyValuePair<bool, string> ReturnValidationAnswer(bool shouldBeExecuted, string text = null)
        {
            return new KeyValuePair<bool, string>(shouldBeExecuted, text);
        }
    }
}