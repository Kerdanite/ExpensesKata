using System;
using System.Threading.Tasks;
using ExpenseKata.Domain.Users;
using ExpenseKata.Infrastructure.Interfaces;

namespace ExpenseKata.Infrastructure.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IApplicationDbContext _context;

        public UserRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}