using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Rules;

namespace ExpenseKata.Domain.Expenses
{
    public class Expense : AggregateRoot, IEquatable<Expense>
    {
        private readonly ExpenseAmount _expenseAmount;
        private readonly DateTime _expenseDate;
        private readonly Guid _userId;
        private readonly ExpenseNature _nature;
        private readonly string _comment;

        private Expense(ExpenseAmount expenseAmount, DateTime expenseDate, Guid userId, ExpenseNature nature, string comment)
        {
            _expenseAmount = expenseAmount;
            _expenseDate = expenseDate;
            _userId = userId;
            _nature = nature;
            _comment = comment;
        }

        public static Expense Create(IDateTimeProvider dateTimeProvider, string comment, DateTime expenseDate, ExpenseUser user, string currency, decimal amount, ExpenseNature nature)
        {
            CheckRule(new ExpenseShouldHaveCommentRule(comment));
            CheckRule(new ExpenseCannotBeInFutureRule(dateTimeProvider, expenseDate));
            CheckRule(new ExpenseCannotBeOlderThanThreeMonthRule(dateTimeProvider, expenseDate));
            var expenseCurrency = new ExpenseCurrency(currency);
            CheckRule(new ExpenseShouldHaveSameCurrencyThanUserRule(user, expenseCurrency.Currency));
            ExpenseAmount expenseAmount = new ExpenseAmount(amount, expenseCurrency);
            CheckRule(new ExpenseShouldBeUniqueOnDateAndAmountPerUserRule(user, expenseDate, expenseAmount));

            return new Expense(expenseAmount, expenseDate, user.Id, nature, comment);
        }

        public ExpenseMemento ToMemento()
        {
            return new ExpenseMemento
            {
                Id = this.Id,
                Currency = _expenseAmount.Currency.Currency,
                UserId = _userId,
                ExpenseDate = _expenseDate,
                Comment = _comment,
                Amount = _expenseAmount.Amount,
                Nature = _nature
            };
        }

        public static Expense FromMemento(ExpenseMemento memento)
        {
            return new Expense(new ExpenseAmount(memento.Amount, new ExpenseCurrency(memento.Currency)), memento.ExpenseDate, memento.UserId, memento.Nature, memento.Comment);
        }

        public bool Equals(Expense other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(_expenseAmount, other._expenseAmount) && _expenseDate.Equals(other._expenseDate) && _userId.Equals(other._userId) && _nature == other._nature && _comment == other._comment;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Expense) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (_expenseAmount != null ? _expenseAmount.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ _expenseDate.GetHashCode();
                hashCode = (hashCode * 397) ^ _userId.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) _nature;
                hashCode = (hashCode * 397) ^ (_comment != null ? _comment.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(Expense left, Expense right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Expense left, Expense right)
        {
            return !Equals(left, right);
        }
    }

    public class ExpenseMemento
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get;set; }
        public DateTime ExpenseDate { get; set; }
        public Guid UserId { get; set; }
        public ExpenseNature Nature { get; set; }
        public string Comment { get; set; }
    }
}