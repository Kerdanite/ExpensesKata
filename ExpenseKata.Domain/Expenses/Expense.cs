using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Rules;

namespace ExpenseKata.Domain.Expenses
{
    public class Expense : AggregateRoot
    {
        private string _comment;

        private Expense()
        {
        }

        public static Expense Create(IDateTimeProvider dateTimeProvider, string comment, DateTime expenseDate)
        {
            CheckRule(new ExpenseShouldHaveCommentRule(comment));
            CheckRule(new ExpenseCannotBeInFutureRule(dateTimeProvider, expenseDate));
            CheckRule(new ExpenseCannotBeOlderThanThreeMonth(dateTimeProvider, expenseDate));

            return null;
        }
    }
}