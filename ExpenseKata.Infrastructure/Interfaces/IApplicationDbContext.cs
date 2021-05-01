using System.Threading;
using System.Threading.Tasks;
using ExpenseKata.Domain.Expenses;
using Microsoft.EntityFrameworkCore;

namespace ExpenseKata.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Expense> Expenses { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}