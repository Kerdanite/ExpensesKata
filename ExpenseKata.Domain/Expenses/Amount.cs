using System;

namespace ExpenseKata.Domain.Expenses
{
    public class ExpenseAmount : IEquatable<ExpenseAmount>
    {
        private readonly decimal _amount;
        private readonly Currency _currency;

        private ExpenseAmount()
        {
        }
        public ExpenseAmount(decimal amount, Currency currency)
        {
            _amount = amount;
            _currency = currency;
        }

        public decimal Amount => _amount;
        public Currency Currency => _currency;

        public bool Equals(ExpenseAmount other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _amount == other._amount && _currency == other._currency;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ExpenseAmount) obj);
        }
    }

}