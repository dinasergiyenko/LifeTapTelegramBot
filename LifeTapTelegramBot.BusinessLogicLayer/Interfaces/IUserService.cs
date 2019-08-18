using System;
using System.Collections.Generic;
using LiteTapTelegramBot.DataAccessLayer.Entities;

namespace LifeTapTelegramBot.BusinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        void Add(int userId, long chatId, string lifeTapUsername);

        IEnumerable<User> GetSubscribers(IEnumerable<string> lifeTapUsernames);

        void Remove(int userId);
    }
}