using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Users;
using ExpenseKata.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpenseKata.Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ExpenseMemento> Expenses { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}