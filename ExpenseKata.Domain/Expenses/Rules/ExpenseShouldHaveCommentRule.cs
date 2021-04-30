using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Constants;

namespace ExpenseKata.Domain.Expenses.Rules
{
    public class ExpenseShouldHaveCommentRule : IBusinessRule
    {
        private readonly string _comment;

        public ExpenseShouldHaveCommentRule(string comment)
        {
            _comment = comment;
        }

        public bool IsBroken() => string.IsNullOrEmpty(_comment);

        public string Message => ExpenseValidationConstants.ExpenseShouldHaveCommentMessage;
    }
}