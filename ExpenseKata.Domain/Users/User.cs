using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses;

namespace ExpenseKata.Domain.Users
{
    public class User : AggregateRoot
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Currency Currency { get; set; }
    }
}