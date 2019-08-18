using System;
using LiteTapTelegramBot.DataAccessLayer.Entities;
using LiteTapTelegramBot.DataAccessLayer.Interfaces;
using LiteTapTelegramBot.DataAccessLayer.Repositories;

namespace LiteTapTelegramBot.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _databaseContext;

        public UnitOfWork(
            DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IRepository<User, int> UserRepository => _userRepository ?? (_userRepository = new UserRepository(_databaseContext));

        private IRepository<User, int> _userRepository;

        public void Commit()
        {
            try
            {
                _databaseContext.SaveChanges();
            }
            catch (Exception e)
            {
                var bla = e;
            }
        }
    }
}