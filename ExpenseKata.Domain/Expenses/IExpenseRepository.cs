using System;
using System.Threading.Tasks;

namespace ExpenseKata.Domain.Expenses
{
    public interface IExpenseRepository
    {
        Task AddAsync(Expense expense);

        Task<Expense> GetByIdAsync(Guid id);
    }
}