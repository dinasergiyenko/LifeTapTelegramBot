using LifeTapTelegramBot.Bot;
using LifeTapTelegramBot.BusinessLogicLayer.Interfaces;
using LifeTapTelegramBot.BusinessLogicLayer.Services;
using LifeTapTelegramBot.Common;
using LiteTapTelegramBot.DataAccessLayer;
using LiteTapTelegramBot.DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LifeTapTelegramBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<BotMessages>(Configuration.GetSection(nameof(BotMessages)));

            var appSettingsSection = Configuration.GetSection(nameof(AppSettings));
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddDbContext<DatabaseContext>(
                options => options
                    .UseSqlServer(appSettings.DbConnectionString));

            services.AddSingleton<IBotService, BotService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IBotService botService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            
            app.UseMvc();

            botService.GetBotClientAsync().Wait();
        }
    }
}
