using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Rules;

namespace ExpenseKata.Domain.Expenses
{
    public class Expense : AggregateRoot
    {
        private readonly ExpenseAmount _expenseAmount;
        private readonly DateTime _expenseDate;
        private readonly ExpenseUser _user;
        private readonly Currency _currency;
        private readonly decimal _amount;
        private readonly ExpenseNature _nature;
        private readonly string _comment;

        private Expense()
        {
        }

        private Expense(ExpenseAmount expenseAmount, DateTime expenseDate, ExpenseUser user, Currency currency, decimal amount, ExpenseNature nature, string comment)
        {
            _expenseAmount = expenseAmount;
            _expenseDate = expenseDate;
            _user = user;
            _currency = currency;
            _amount = amount;
            _nature = nature;
            _comment = comment;
        }

        public static Expense Create(IDateTimeProvider dateTimeProvider, string comment, DateTime expenseDate, ExpenseUser user, Currency currency, decimal amount, ExpenseNature nature)
        {
            CheckRule(new ExpenseShouldHaveCommentRule(comment));
            CheckRule(new ExpenseCannotBeInFutureRule(dateTimeProvider, expenseDate));
            CheckRule(new ExpenseCannotBeOlderThanThreeMonthRule(dateTimeProvider, expenseDate));
            CheckRule(new ExpenseShouldHaveSameCurrencyThanUserRule(user, currency));

            ExpenseAmount expenseAmount = new ExpenseAmount(amount, currency);
            CheckRule(new ExpenseShouldBeUniqueOnDateAndAmountPerUserRule(user, expenseDate, expenseAmount));

            return new Expense(expenseAmount, expenseDate, user, currency, amount, nature, comment);
        }
    }
}