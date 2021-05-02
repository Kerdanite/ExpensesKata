using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Constants;

namespace ExpenseKata.Domain.Expenses
{
    public class CurrencyValueExistRule : IBusinessRule
    {
        private readonly string _currency;

        public CurrencyValueExistRule(string currency)
        {
            _currency = currency;
        }

        public bool IsBroken() => !Enum.IsDefined(typeof(Currency), _currency);

        public string Message => string.Format(ExpenseValidationConstants.CurrencyValueExist, _currency);
    }
}