using TMA.Domain.Entities;

namespace TMA.Domain.Interfaces.Repositories
{
    public interface IUsersRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetByEmail(string email);
        Task<UserEntity?> GetByEmailAndPassword(string email, string password);
    }
}
