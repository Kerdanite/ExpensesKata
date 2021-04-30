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

        public static Expense Create(IDateTimeProvider dateTimeProvider, string comment, DateTime expenseDate, ExpenseUser user, Currency currency)
        {
            CheckRule(new ExpenseShouldHaveCommentRule(comment));
            CheckRule(new ExpenseCannotBeInFutureRule(dateTimeProvider, expenseDate));
            CheckRule(new ExpenseCannotBeOlderThanThreeMonth(dateTimeProvider, expenseDate));
            CheckRule(new ExpenseShouldHaveSameCurrencyThanUser(user, currency));

            return null;
        }
    }
}