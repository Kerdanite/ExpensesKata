using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Rules;

namespace ExpenseKata.Domain.Expenses
{
    public class Expense : AggregateRoot
    {
        private readonly ExpenseAmount _expenseAmount;
        private readonly DateTime _expenseDate;
        private readonly Guid _userId;
        private readonly ExpenseNature _nature;
        private readonly string _comment;

        private Expense(ExpenseAmount expenseAmount, DateTime expenseDate, Guid userId, ExpenseNature nature, string comment)
        {
            _expenseAmount = expenseAmount;
            _expenseDate = expenseDate;
            _userId = userId;
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

            return new Expense(expenseAmount, expenseDate, user.Id, nature, comment);
        }

        public ExpenseMemento ToMemento()
        {
            return new ExpenseMemento
            {
                Id = this.Id,
                Currency = _expenseAmount.Currency,
                UserId = _userId,
                ExpenseDate = _expenseDate,
                Comment = _comment,
                Amount = _expenseAmount.Amount,
                Nature = _nature
            };
        }

        public static Expense FromMemento(ExpenseMemento memento)
        {
            return new Expense(new ExpenseAmount(memento.Amount, memento.Currency), memento.ExpenseDate, memento.UserId, memento.Nature, memento.Comment);
        }
    }

    public class ExpenseMemento
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get;set; }
        public DateTime ExpenseDate { get; set; }
        public Guid UserId { get; set; }
        public ExpenseNature Nature { get; set; }
        public string Comment { get; set; }
    }
}