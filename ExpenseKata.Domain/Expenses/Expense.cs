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

        public static Expense Create(string comment)
        {
            CheckRule(new ExpenseShouldHaveCommentRule(comment));
            return null;
        }
    }
}