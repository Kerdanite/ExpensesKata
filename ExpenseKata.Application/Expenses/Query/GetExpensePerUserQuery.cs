using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExpenseKata.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseKata.Application.Expenses.Query
{
    public class GetExpensePerUserQuery : IRequest<IEnumerable<ExpenseDto>>
    {
        public Guid UserId { get; set; }
    }

    public class GetExpensePerUserQueryHandler : IRequestHandler<GetExpensePerUserQuery, IEnumerable<ExpenseDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetExpensePerUserQueryHandler(IApplicationDbContext context)
        {
            _applicationDbContext = context;
        }


        public async Task<IEnumerable<ExpenseDto>> Handle(GetExpensePerUserQuery request, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.Expenses
                .Join(_applicationDbContext.Users, expense => expense.UserId, user => user.Id, (expense, user) => new {expense, user})
                .Select(se => new ExpenseDto
                {
                    Amount = se.expense.Amount,
                    ExpenseDate = se.expense.ExpenseDate,
                    Comment = se.expense.Comment,
                    Nature = se.expense.Nature.ToString(),
                    Currency = se.expense.Currency.ToString(),
                    User = $"{se.user.FirstName} {se.user.LastName}"
                });

            return await query.ToListAsync(cancellationToken);
        }
    }
}