using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Users;
using ExpenseKata.Infrastructure.Expenses;
using ExpenseKata.Infrastructure.Interfaces;
using ExpenseKata.Infrastructure.Persistance;
using ExpenseKata.Infrastructure.Service;
using ExpenseKata.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExpenseKata.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IUnitOfWork>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IExpenseRepository, ExpenseRepository>();

            return services;
        }
    }
}