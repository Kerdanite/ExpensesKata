using System;
using ExpenseKata.Domain.Common;

namespace ExpenseKata.Domain.Expenses
{
    public class ExpenseCurrency : ValueObject<ExpenseCurrency>
    {
        private readonly Currency _currency;

        public ExpenseCurrency(string currency)
        {
            CheckRule(new CurrencyValueExistRule(currency));
            _currency = Enum.Parse<Currency>(currency);
        }

        internal ExpenseCurrency(Currency mementoCurrency)
        {
            _currency = mementoCurrency;
        }

        public Currency Currency => _currency;

        public override bool Equals(ExpenseCurrency other)
        {
            return _currency == other._currency;
        }
    }
}