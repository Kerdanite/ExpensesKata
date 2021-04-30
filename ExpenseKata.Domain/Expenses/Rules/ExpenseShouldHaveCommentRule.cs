using ExpenseKata.Domain.Common;

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

        public string Message => "Le commentaire est obligatoire";
    }
}