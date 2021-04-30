using System;

namespace ExpenseKata.Domain.Common
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}