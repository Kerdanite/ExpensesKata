using System;

namespace ExpenseKata.Application.Expenses.Query
{
    public class ExpenseDto
    {
        public string User { get; set; }

        public DateTime ExpenseDate { get;set; }

        public string Nature { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get;set; }

        public string Comment { get; set; }
    }
}