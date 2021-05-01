using System;
using ExpenseKata.Domain.Common;

namespace ExpenseKata.Infrastructure.Service
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}