using System;

namespace ExpenseKata.Domain.Expenses
{
    public class UserExpenseHistory
    {
        private readonly DateTime _expenseDate;
        private readonly decimal _amount;

        private UserExpenseHistory()
        {
        }

        public UserExpenseHistory(DateTime expenseDate, decimal amount)
        {
            _expenseDate = expenseDate;
            _amount = amount;
        }

        public decimal Amount => _amount;
        public DateTime ExpenseDate => _expenseDate;
    }
}