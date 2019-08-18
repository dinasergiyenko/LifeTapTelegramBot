using System;
using System.Collections.Generic;
using System.Linq;
using LifeTapTelegramBot.BusinessLogicLayer.Interfaces;
using LiteTapTelegramBot.DataAccessLayer.Entities;
using LiteTapTelegramBot.DataAccessLayer.Interfaces;

namespace LifeTapTelegramBot.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(int userId, long chatId, string lifeTapUsername)
        {
            var existingUser = GetUser(userId);

            if (existingUser == null)
            {
                var user = new User
                {
                    UserId = userId,
                    ChatId = chatId,
                    LifeTapUsername = lifeTapUsername
                };

                _unitOfWork.UserRepository.Add(user);
            }
            else
            {
                existingUser.ChatId = chatId;
                existingUser.LifeTapUsername = lifeTapUsername;
                existingUser.IsSubscribed = true;
            }

            _unitOfWork.Commit();
        }

        public IEnumerable<User> GetSubscribers(IEnumerable<string> lifeTapUserIds)
        {
            return _unitOfWork.UserRepository
                .Find(x => lifeTapUserIds.Contains(x.LifeTapUsername));
        }

        public void Remove(int userId)
        {
            var existingUser = GetUser(userId);

            if (existingUser != null)
            {
                existingUser.IsSubscribed = false;
            }

            _unitOfWork.Commit();
        }

        private User GetUser(int userId)
        {
            return _unitOfWork.UserRepository
                .Find(x => x.UserId == userId)
                .FirstOrDefault();
        }
    }
}