using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ExpenseKata.Infrastructure.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ExpenseMemento> Expenses { get; set; }
        DbSet<User> Users { get; set; }
    }
}