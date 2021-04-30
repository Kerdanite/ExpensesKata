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

        public static Expense Create(IDateTimeProvider dateTimeProvider, string comment, DateTime expenseDate, ExpenseUser user, Currency currency, decimal amount)
        {
            CheckRule(new ExpenseShouldHaveCommentRule(comment));
            CheckRule(new ExpenseCannotBeInFutureRule(dateTimeProvider, expenseDate));
            CheckRule(new ExpenseCannotBeOlderThanThreeMonthRule(dateTimeProvider, expenseDate));
            CheckRule(new ExpenseShouldHaveSameCurrencyThanUserRule(user, currency));

            ExpenseAmount expenseAmount = new ExpenseAmount(amount, currency);
            CheckRule(new ExpenseShouldBeUniqueOnDateAndAmountPerUserRule(user, expenseDate, expenseAmount));

            return null;
        }
    }
}