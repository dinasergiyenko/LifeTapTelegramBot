using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LiteTapTelegramBot.DataAccessLayer.Entities
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        public long ChatId { get; set; }

        public string LifeTapUsername { get; set; }

        public bool IsSubscribed { get; set; }
    }
}