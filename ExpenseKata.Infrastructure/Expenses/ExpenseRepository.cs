using System;
using System.Threading.Tasks;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Infrastructure.Interfaces;

namespace ExpenseKata.Infrastructure.Expenses
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly IApplicationDbContext _context;

        public ExpenseRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
        }

        public async Task<Expense> GetByIdAsync(Guid id)
        {
            return await _context.Expenses.FindAsync(id);
        }
    }
}