using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses;
using Xunit;

namespace ExpenseKata.Domain.Tests
{
    public class ExpenseTests
    {
        [Fact]
        public void CreateExpense_EmptyCommentaryShoudThrowException()
        {
            var exception = Assert.Throws<BusinessRuleValidationException>(() => Expense.Create(""));
            Assert.Equal("Le commentaire est obligatoire", exception.Message);
        }
    }
}