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

        public bool IsBroken() => IsExpenseInFuture(_provider.Now, _expenseDate);

        private bool IsExpenseInFuture(DateTime date1, DateTime date2)
        {
            return date1 < date2;
        }

        public string Message => ExpenseValidationConstants.ExpenseCannotBeInFuture;
    }
}