using System;
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

        public EpenseBuilder()
        {
            SetDefaultValue();
        }

        private void SetDefaultValue()
        {
            _comment = "default";
            _dateTimeProvider = new DateTimeProviderStub(new DateTime(2021, 5, 1));
            _expenseDate = new DateTime(2021, 4, 1);
        }

        public Expense Build()
        {
            return Expense.Create(_dateTimeProvider, _comment, _expenseDate);
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
    }
}