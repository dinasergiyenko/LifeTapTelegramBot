using System;
using System.Collections.Generic;

namespace LifeTapTelegramBot.ViewModels
{
    public class EventViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public IEnumerable<string> Usernames { get; set; }
    }
}