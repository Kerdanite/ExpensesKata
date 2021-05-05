using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExpenseKata.Application.Expenses.Query
{
    internal class ExpenseQueryOrdering
    {
        private readonly List<string> _allowedOrderingParam = new List<string>
        {
            nameof(ExpenseDto.Amount),
            nameof(ExpenseDto.ExpenseDate)
        };

        private OrderingAction _orderingAction;

        public ExpenseQueryOrdering(string orderParam)
        {
            if (_allowedOrderingParam.Any(a => orderParam.Contains(a, StringComparison.InvariantCultureIgnoreCase)))
            {
                _orderingAction = new OrderingAction(orderParam);
            }
        }

        public List<ExpenseDto> OrderResult(List<ExpenseDto> result)
        {
            if (_orderingAction == null)
            {
                return OrderResultForNoOrderingParam(result);
            }
            return OrderResultForOneOrderingParam(result);
        }

        private List<ExpenseDto> OrderResultForNoOrderingParam(List<ExpenseDto> result)
        {
            return result;
        }

        private List<ExpenseDto> OrderResultForOneOrderingParam(List<ExpenseDto> result)
        {
            if (_orderingAction.OrderingDirection == OrderingDirection.Ascending)
            {
                return result.OrderBy(or => _orderingAction.OrderingField.GetValue(or)).ToList();
            }

            return result.OrderByDescending(or => _orderingAction.OrderingField.GetValue(or)).ToList();
        }
    }

    internal class OrderingAction
    {
        public OrderingAction(string orderParam)
        {
            OrderingDirection = orderParam.EndsWith("desc") ? OrderingDirection.Descending : OrderingDirection.Ascending;
            var field = orderParam.Split(" ")[0];
            OrderingField = typeof(ExpenseDto).GetProperty(field, BindingFlags.IgnoreCase |  BindingFlags.Public | BindingFlags.Instance);
        }

        public PropertyInfo OrderingField { get; private set; }
        public OrderingDirection OrderingDirection { get; private set; }
    }

    internal enum OrderingDirection
    {
        Ascending,
        Descending
    }
}