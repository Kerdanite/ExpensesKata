using System;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses.Constants;

namespace ExpenseKata.Domain.Expenses.Rules
{
    public class ExpenseNatureTypeExistRule : IBusinessRule
    {
        private readonly string _nature;

        public ExpenseNatureTypeExistRule(string nature)
        {
            _nature = nature;
        }

        public bool IsBroken() => !Enum.IsDefined(typeof(ExpenseNatureType), _nature);
       
        public string Message  => string.Format(ExpenseValidationConstants.ExpenseNatureExist, _nature);
    }
}