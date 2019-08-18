using LiteTapTelegramBot.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace LiteTapTelegramBot.DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
    }
}