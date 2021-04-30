using System;
using System.Collections.Generic;

namespace ExpenseKata.Domain.Common
{
    public abstract class AggregateRoot
    {
        public Guid Id { get; protected set; }

        protected AggregateRoot()
        {
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            if (rule.IsBroken())
            {
                throw new BusinessRuleValidationException(rule);
            }
        }
    }
}