using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Constants;

namespace ExpenseKata.Domain.Expenses.Rules
{
    public class ExpenseCannotBeOlderThanThreeMonth : IBusinessRule
    {
        private readonly IDateTimeProvider _provider;
        private readonly DateTime _expenseDate;

        public ExpenseCannotBeOlderThanThreeMonth(IDateTimeProvider provider, DateTime expenseDate)
        {
            _provider = provider;
            _expenseDate = expenseDate;
        }

        public bool IsBroken() => IsExpenseOlderThanThreeMonth(_provider.Now, _expenseDate);

        private bool IsExpenseOlderThanThreeMonth(DateTime date1, DateTime date2)
        {
            return MonthDifference(date1, date2) > 3;
        }

        private int MonthDifference(DateTime date1, DateTime date2)
        {
            return (date1.Month - date2.Month) + 12 * (date1.Year - date2.Year);
        }

        public string Message => ExpenseValidationConstants.ExpenseCannotBeOlderThanThreeMonth;
    }
}