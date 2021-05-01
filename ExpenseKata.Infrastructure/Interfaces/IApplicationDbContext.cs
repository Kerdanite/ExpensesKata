using System.Threading;
using System.Threading.Tasks;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ExpenseKata.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Expense> Expenses { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}