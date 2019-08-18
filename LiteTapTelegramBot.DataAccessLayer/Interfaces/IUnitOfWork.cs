using LiteTapTelegramBot.DataAccessLayer.Entities;

namespace LiteTapTelegramBot.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User, int> UserRepository { get; }

        void Commit();
    }
}