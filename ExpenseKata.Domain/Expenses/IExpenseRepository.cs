using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseKata.Domain.Expenses
{
    public interface IExpenseRepository
    {
        Task AddAsync(Expense expense);

        Task<Expense> GetByIdAsync(Guid id);

        Task<IEnumerable<UserExpenseHistory>> GetExpenseHistoryForUser(Guid requestUserId);
    }
}