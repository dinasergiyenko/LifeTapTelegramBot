using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LiteTapTelegramBot.DataAccessLayer.Entities;
using LiteTapTelegramBot.DataAccessLayer.Interfaces;

namespace LiteTapTelegramBot.DataAccessLayer.Repositories
{
    public class UserRepository : GenericRepository<User, int>
    {
        public UserRepository(
            DatabaseContext db
            ) : base(db)
        { }

    }
}