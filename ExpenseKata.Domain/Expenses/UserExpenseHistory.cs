using System;

namespace ExpenseKata.Domain.Expenses
{
    public struct UserExpenseHistory
    {
        private readonly DateTime _expenseDate;
        private readonly ExpenseAmount _amount;

        public UserExpenseHistory(DateTime expenseDate, ExpenseAmount amount)
        {
            _expenseDate = expenseDate;
            _amount = amount;
        }

        public ExpenseAmount Amount => _amount;
        public DateTime ExpenseDate => _expenseDate;
    }
}