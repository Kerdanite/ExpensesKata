using System;

namespace ExpenseKata.Domain.Expenses
{
    public struct ExpenseUser
    {
        private readonly Guid _userId;
        private readonly Currency _currency;

        public ExpenseUser(Guid userId, Currency currency)
        {
            _userId = userId;
            _currency = currency;
        }

        public Currency  Currency => _currency;
    }
}