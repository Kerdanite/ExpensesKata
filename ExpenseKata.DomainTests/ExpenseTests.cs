using System;
using System.Collections.Generic;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Expenses.Constants;
using Xunit;

namespace ExpenseKata.DomainTests
{
    public class ExpenseTests
    {
        [Fact]
        public void CreateExpense_EmptyCommentaryShoudThrowException()
        {
            var builder = new EpenseBuilder();
            builder.WithComment(string.Empty);
            var exception = Assert.Throws<BusinessRuleValidationException>(() => builder.Build());
            Assert.Equal(ExpenseValidationConstants.ExpenseShouldHaveCommentMessage, exception.Message);
        }

        [Fact]
        public void CreateExpense_FutureExpenseShoudThrowException()
        {
            DateTime expenseDate = new DateTime(2021, 6, 1);
            var provider = new DateTimeProviderStub(new DateTime(2021, 5, 1));

            var builder = new EpenseBuilder();
            builder.WithExpenseDate(expenseDate);
            builder.WithDateTimeProvider(provider);
            var exception = Assert.Throws<BusinessRuleValidationException>(() => builder.Build());
            Assert.Equal(ExpenseValidationConstants.ExpenseCannotBeInFuture, exception.Message);
        }

        [Fact]
        public void CreateExpense_PastExpense_ShoudNotThrowException()
        {
            DateTime expenseDate = new DateTime(2021, 4, 25);
            var provider = new DateTimeProviderStub(new DateTime(2021, 5, 1));

            var builder = new EpenseBuilder();
            builder.WithExpenseDate(expenseDate);
            builder.WithDateTimeProvider(provider);
            var exception = Record.Exception(() => builder.Build());
            Assert.Null(exception);
        }

        [Fact]
        public void CreateExpense_ExpenseOlderThan3Month_ShouldThrowException()
        {
            DateTime expenseDate = new DateTime(2021, 1, 25);
            var provider = new DateTimeProviderStub(new DateTime(2021, 5, 1));

            var builder = new EpenseBuilder();
            builder.WithExpenseDate(expenseDate);
            builder.WithDateTimeProvider(provider);

            var exception = Assert.Throws<BusinessRuleValidationException>(() => builder.Build());
            Assert.Equal(ExpenseValidationConstants.ExpenseCannotBeOlderThanThreeMonth, exception.Message);
        }

        [Fact]
        public void CreateExpense_ExpenseWithDateWith3Month_ShouldNotThrowException()
        {
            DateTime expenseDate = new DateTime(2021, 1, 1);
            var provider = new DateTimeProviderStub(new DateTime(2021, 4, 30));

            var builder = new EpenseBuilder();
            builder.WithExpenseDate(expenseDate);
            builder.WithDateTimeProvider(provider);

            var exception = Record.Exception(() => builder.Build());
            Assert.Null(exception);
        }

        [Fact]
        public void CreateExpense_ExpenseWithDifferentCurrencyFromUser_ShouldThrowException()
        {
            var builder = new EpenseBuilder();
            builder.WithUser(new ExpenseUser(Guid.NewGuid(), Currency.Dollar, new List<UserExpenseHistory>()));
            builder.WithCurrency(Currency.Euro);

            var exception = Assert.Throws<BusinessRuleValidationException>(() => builder.Build());
            Assert.Equal(ExpenseValidationConstants.ExpenseShouldHaveSameCurrencyThanUser, exception.Message);
        }

        [Fact]
        public void CreateExpense_UserWithSameExpenseTwice_ShouldThrowException()
        {
            var builder = new EpenseBuilder();
            var expenseDate = new DateTime(2021, 5, 1);
            decimal amount = 30;
            builder.WithUser(new ExpenseUser(Guid.NewGuid(), Currency.Dollar, new List<UserExpenseHistory>
            {
                new UserExpenseHistory(expenseDate, new ExpenseAmount(amount, Currency.Dollar))
            }));
            builder.WithExpenseDate(expenseDate);
            builder.WithAmount(amount);

            var exception = Assert.Throws<BusinessRuleValidationException>(() => builder.Build());
            Assert.Equal(ExpenseValidationConstants.ExpenseShouldBeUniqueOnDateAndAmountPerUser, exception.Message);
        }

        [Fact]
        public void CreateExpense_ShouldReturnCreatedUser()
        {
            var builder = new EpenseBuilder();

            var expense = builder.Build();
            Assert.NotNull(expense);
            Assert.NotEqual(Guid.Empty, expense.Id);
        }

        [Fact]
        public void CreateExpense_VerifyMemento()
        {
            var expenseDate = new DateTime(2021, 04, 01);
            decimal amount = 22;
            var comment = "TestComment";
            var currency = Currency.Dollar;
            var builder = new EpenseBuilder();
            var userId = Guid.NewGuid();
            var nature = ExpenseNature.Restaurant;

            builder.WithExpenseDate(expenseDate);
            builder.WithAmount(amount);
            builder.WithComment(comment);
            builder.WithCurrency(currency);
            builder.WithUser(new ExpenseUser(userId, currency, new List<UserExpenseHistory>()));
            builder.WithNature(nature);

            var expense = builder.Build();
            var memento = expense.ToMemento();

            Assert.Equal(expenseDate, memento.ExpenseDate);
            Assert.Equal(amount, memento.Amount);
            Assert.Equal(comment, memento.Comment);
            Assert.Equal(currency, memento.Currency);
            Assert.Equal(userId, memento.UserId);
            Assert.Equal(nature, memento.Nature);
        }


        [Fact]
        public void CreateExpense_VerifyToMemento()
        {
            var builder = new EpenseBuilder();
            var expense = builder.Build();
            var memento = expense.ToMemento();

            var fromMemento = Expense.FromMemento(memento);

            Assert.Equal(expense, fromMemento);
        }
    }

    public class DateTimeProviderStub : IDateTimeProvider
    {
        private readonly DateTime _testDate;

        public DateTimeProviderStub(DateTime testDate)
        {
            _testDate = testDate;
        }

        public DateTime Now => _testDate;
    }

    internal class EpenseBuilder
    {
        private string _comment;
        private IDateTimeProvider _dateTimeProvider;
        private DateTime _expenseDate;
        private ExpenseUser _user;
        private Currency _currency;
        private decimal _amount;
        private ExpenseNature _nature;

        public EpenseBuilder()
        {
            SetDefaultValue();
        }

        private void SetDefaultValue()
        {
            _comment = "default";
            _dateTimeProvider = new DateTimeProviderStub(new DateTime(2021, 5, 1));
            _expenseDate = new DateTime(2021, 4, 1);
            _currency = Currency.Dollar;
            _user = new ExpenseUser(Guid.NewGuid(), Currency.Dollar, new List<UserExpenseHistory>());
            _amount = 15;
            _nature = ExpenseNature.Hotel;
        }

        public Expense Build()
        {
            return Expense.Create(_dateTimeProvider, _comment, _expenseDate, _user, _currency, _amount, _nature);
        }

        public void WithComment(string comment)
        {
            _comment = comment;
        }

        public void WithExpenseDate(DateTime date)
        {
            _expenseDate = date;
        }

        public void WithDateTimeProvider(IDateTimeProvider provider)
        {
            _dateTimeProvider = provider;
        }

        public void WithCurrency(Currency currency)
        {
            _currency = currency;
        }

        public void WithUser(ExpenseUser user)
        {
            _user = user;
        }

        public void WithAmount(decimal amount)
        {
            _amount = amount;
        }

        public void WithNature(ExpenseNature nature)
        {
            _nature = nature;
        }
    }
}