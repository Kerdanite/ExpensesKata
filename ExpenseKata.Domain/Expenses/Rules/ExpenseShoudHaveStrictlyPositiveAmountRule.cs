using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Constants;

namespace ExpenseKata.Domain.Expenses.Rules
{
    public class ExpenseShoudHaveStrictlyPositiveAmountRule : IBusinessRule
    {
        private readonly decimal _amount;

        public ExpenseShoudHaveStrictlyPositiveAmountRule(decimal amount)
        {
            _amount = amount;
        }

        public bool IsBroken() => !(_amount > 0);

        public string Message => ExpenseValidationConstants.ExpenseAmountShouldBeMoreThanZero;
    }
}