using System;

namespace ExpenseKata.Domain.Expenses
{
    public struct ExpenseAmount
    {
        private readonly decimal _amount;
        private readonly Currency _currency;

        public ExpenseAmount(decimal amount, Currency currency)
        {
            _amount = amount;
            _currency = currency;
        }

        public decimal Amount => _amount;
    }

}