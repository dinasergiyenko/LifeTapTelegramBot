using System;
using System.Collections.Generic;

namespace LifeTapTelegramBot.ViewModels
{
    public class EventViewModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Usernames { get; set; }
    }
}