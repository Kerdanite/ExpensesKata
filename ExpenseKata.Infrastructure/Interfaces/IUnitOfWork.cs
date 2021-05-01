using System.Threading;
using System.Threading.Tasks;

namespace ExpenseKata.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}