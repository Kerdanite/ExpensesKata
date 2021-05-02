using System;
using System.Linq;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Constants;

namespace ExpenseKata.Domain.Expenses.Rules
{
    public class ExpenseShouldBeUniqueOnDateAndAmountPerUserRule : IBusinessRule
    {
        private readonly ExpenseUser _user;
        private readonly DateTime _expenseDate;
        private readonly ExpenseAmount _amount;

        public ExpenseShouldBeUniqueOnDateAndAmountPerUserRule(ExpenseUser user, DateTime expenseDate, ExpenseAmount amount)
        {
            _user = user;
            _expenseDate = expenseDate;
            _amount = amount;
        }

        public bool IsBroken()
        {
            return _user.Histories.Any(o => o.ExpenseDate == _expenseDate && o.Amount.Equals(_amount.Amount));
        }

        public string Message => ExpenseValidationConstants.ExpenseShouldBeUniqueOnDateAndAmountPerUser;
    }
}