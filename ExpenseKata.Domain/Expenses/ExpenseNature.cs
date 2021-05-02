using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Rules;

namespace ExpenseKata.Domain.Expenses
{
    public class ExpenseNature : ValueObject<ExpenseNature>
    {
        private readonly ExpenseNatureType _expenseNature;
        internal ExpenseNature(string nature)
        {
            CheckRule(new ExpenseNatureTypeExistRule(nature));
            _expenseNature = Enum.Parse<ExpenseNatureType>(nature);
        }

        private ExpenseNature(ExpenseNatureType mementoNature)
        {
            _expenseNature = mementoNature;
        }

        internal static ExpenseNature FromMemento(ExpenseNatureType mementoNature)
        {
            return new ExpenseNature(mementoNature);
        }

        public ExpenseNatureType ExpenseNatureType => _expenseNature;

        public override bool Equals(ExpenseNature other)
        {
            return _expenseNature == other._expenseNature;
        }
    }
}