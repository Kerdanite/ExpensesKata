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
            string comment = string.Empty;
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Expense.Create(null, comment, new DateTime()));
            Assert.Equal(ExpenseValidationConstants.ExpenseShouldHaveCommentMessage, exception.Message);
        }

        [Fact]
        public void CreateExpense_FutureExpenseShoudThrowException()
        {
            string comment = "Default";
            DateTime expenseDate = new DateTime(2021, 6, 1);
            var provider = new DateTimeProviderStub(new DateTime(2021, 5, 1));
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Expense.Create(provider, comment, expenseDate));
            Assert.Equal(ExpenseValidationConstants.ExpenseCannotBeInFuture, exception.Message);
        }

        [Fact]
        public void CreateExpense_PastExpense_ShoudNotThrowException()
        {
            string comment = "Default";
            DateTime expenseDate = new DateTime(2021, 4, 25);
            var provider = new DateTimeProviderStub(new DateTime(2021, 5, 1));


            var exception = Record.Exception(() => Expense.Create(provider, comment, expenseDate));
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
}