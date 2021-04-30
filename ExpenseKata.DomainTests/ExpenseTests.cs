using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Expenses.Constants;
using Xunit;

namespace ExpenseKata.Domain.Tests
{
    public class ExpenseTests
    {
        [Fact]
        public void CreateExpense_EmptyCommentaryShoudThrowException()
        {
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Expense.Create(string.Empty));
            Assert.Equal(ExpenseValidationConstants.ExpenseShouldHaveCommentMessage, exception.Message);
        }
    }
}