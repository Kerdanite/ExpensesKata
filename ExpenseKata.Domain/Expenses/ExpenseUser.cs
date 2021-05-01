using System;
using System.Collections.Generic;

namespace ExpenseKata.Domain.Expenses
{
    public class ExpenseUser
    {
        private readonly Guid _userId;
        private readonly Currency _currency;
        private readonly IEnumerable<UserExpenseHistory> _histories;

        private ExpenseUser()
        {
        }
        public ExpenseUser(Guid userId, Currency currency, IEnumerable<UserExpenseHistory> histories)
        {
            _userId = userId;
            _currency = currency;
            _histories = histories;
        }

        public Currency  Currency => _currency;
        public IEnumerable<UserExpenseHistory> Histories => _histories;
    }
}