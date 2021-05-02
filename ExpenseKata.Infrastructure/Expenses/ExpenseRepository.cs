using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            await _context.Expenses.AddAsync(expense.ToMemento());
        }

        public async Task<Expense> GetByIdAsync(Guid id)
        {
            return Expense.FromMemento(await _context.Expenses.FindAsync(id));
        }

        public async Task<IEnumerable<UserExpenseHistory>> GetExpenseHistoryForUser(Guid requestUserId)
        {
            return await _context.Expenses
                .Where(w => w.UserId == requestUserId)
                .Select(se => new UserExpenseHistory(se.ExpenseDate, se.Amount))
                .ToListAsync();

        }
    }
}