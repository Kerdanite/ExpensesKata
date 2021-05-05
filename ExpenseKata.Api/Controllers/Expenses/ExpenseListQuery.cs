using System;

namespace ExpenseKata.Api.Controllers.Expenses
{
    public class ExpenseListQuery
    {
        public string OrderBy { get; set; }

        public Guid? UserId { get; set; }
    }
}