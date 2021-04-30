using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Constants;

namespace ExpenseKata.Domain.Expenses.Rules
{
    public class ExpenseShouldHaveSameCurrencyThanUserRule : IBusinessRule
    {
        private readonly ExpenseUser _user;
        private readonly Currency _currency;

        public ExpenseShouldHaveSameCurrencyThanUserRule(ExpenseUser user, Currency currency)
        {
            _user = user;
            _currency = currency;
        }

        public bool IsBroken() => _user.Currency != _currency;

        public string Message => ExpenseValidationConstants.ExpenseShouldHaveSameCurrencyThanUser;
    }
}