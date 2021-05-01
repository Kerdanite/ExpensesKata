using System;
using System.Threading.Tasks;

namespace ExpenseKata.Domain.Users
{
    public interface IUserRepository
    {
        Task AddAsync(User user);

        Task<User> GetByIdAsync(Guid id);
    }
}