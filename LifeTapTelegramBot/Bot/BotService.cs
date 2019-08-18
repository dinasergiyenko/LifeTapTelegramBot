using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LifeTapTelegramBot.Commands;
using LifeTapTelegramBot.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace LifeTapTelegramBot.Bot
{
    public class BotService : IBotService
    {
        private readonly AppSettings _appSettings;
        private readonly IServiceProvider _serviceProvider;

        private TelegramBotClient _botClient;
        private List<CommandBase> _commands;


        public BotService(
            IOptions<AppSettings> appSettings,
            IServiceProvider serviceProvider)
        {
            _appSettings = appSettings.Value;
            _serviceProvider = serviceProvider;
        }

        public async Task<TelegramBotClient> GetBotClientAsync()
        {
            if (_botClient != null)
            {
                return _botClient;
            }

            _botClient = new TelegramBotClient(_appSettings.Token);

            await _botClient.SetWebhookAsync(_appSettings.Url);

            return _botClient;
        }

        public IEnumerable<CommandBase> GetCommands()
        {
            if (_commands != null)
            {
                return _commands.AsEnumerable();
            }

            var commands = Assembly.GetAssembly(typeof(CommandBase))
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(CommandBase)))
                .Select(x =>
                {
                    var constructor = x.GetConstructors()[0];
                    var args = constructor
                        .GetParameters()
                        .Select(o => o.ParameterType)
                        .Select(o => _serviceProvider.GetService(o))
                        .ToArray();

                    return Activator.CreateInstance(x, args) as CommandBase;
                });

            _commands = new List<CommandBase>(commands);

            return _commands.AsEnumerable();
        }
    }
}