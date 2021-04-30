using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Constants;

namespace ExpenseKata.Domain.Expenses.Rules
{
    public class ExpenseCannotBeInFutureRule : IBusinessRule
    {
        private readonly IDateTimeProvider _provider;
        private readonly DateTime _expenseDate;

        public ExpenseCannotBeInFutureRule(IDateTimeProvider provider, DateTime expenseDate)
        {
            _provider = provider;
            _expenseDate = expenseDate;
        }

        public bool IsBroken() => IsDifferentDate(_provider.Now, _expenseDate);

        private bool IsDifferentDate(DateTime providerNow, DateTime expenseDate)
        {
            return true;
        }

        public string Message => ExpenseValidationConstants.ExpenseCannotBeInFuture;
    }
}