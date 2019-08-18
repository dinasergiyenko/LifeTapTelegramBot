using System.Collections.Generic;
using System.Threading.Tasks;
using LifeTapTelegramBot.Commands;
using Telegram.Bot;

namespace LifeTapTelegramBot.Bot
{
    public interface IBotService
    {
        Task<TelegramBotClient> GetBotClientAsync();

        IEnumerable<CommandBase> GetCommands();
    }
}