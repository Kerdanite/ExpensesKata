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

        private ExpenseCurrency(Currency mementoCurrency)
        {
            _currency = mementoCurrency;
        }

        internal static ExpenseCurrency FromMemento(Currency mementoCurrency)
        {
            return new ExpenseCurrency(mementoCurrency);
        }

        public Currency Currency => _currency;

        public override bool Equals(ExpenseCurrency other)
        {
            return _currency == other._currency;
        }
    }
}