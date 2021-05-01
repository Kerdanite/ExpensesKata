using ExpenseKata.Domain.Expenses;
using ExpenseKata.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseKata.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}