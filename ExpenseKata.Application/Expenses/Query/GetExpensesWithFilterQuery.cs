using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpenseKata.Application.Expenses.Query
{
    public class GetExpensesWithFilterQuery : IRequest<IEnumerable<ExpenseDto>>
    {
        public Guid? UserId { get; set; }

        public string OrderBy { get; set; }
    }

    public class GetExpensesWithFilterQueryHandler : IRequestHandler<GetExpensesWithFilterQuery, IEnumerable<ExpenseDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetExpensesWithFilterQueryHandler(IApplicationDbContext context)
        {
            _applicationDbContext = context;
        }


        public async Task<IEnumerable<ExpenseDto>> Handle(GetExpensesWithFilterQuery request, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.Expenses.AsQueryable();

            query = FilterQuery(request, query);
            var queryJoin  = query.Join(_applicationDbContext.Users, expense => expense.UserId, user => user.Id, (expense, user) => new {expense, user});
            IQueryable<ExpenseDto> selectQueryable = queryJoin.Select(se => new ExpenseDto
            {
                Amount = se.expense.Amount,
                ExpenseDate = se.expense.ExpenseDate,
                Comment = se.expense.Comment,
                Nature = se.expense.Nature.ToString(),
                Currency = se.expense.Currency.ToString(),
                User = $"{se.user.FirstName} {se.user.LastName}"
            });
            List<ExpenseDto> result = await selectQueryable.ToListAsync(cancellationToken);
            return OrderResult(request, result);
        }

        private IEnumerable<ExpenseDto> OrderResult(GetExpensesWithFilterQuery request, List<ExpenseDto> result)
        {
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                ExpenseQueryOrdering expenseQueryOrdering = new ExpenseQueryOrdering(request.OrderBy);
                result = expenseQueryOrdering.OrderResult(result);
            }
            return result;
        }

        private static IQueryable<ExpenseMemento> FilterQuery(GetExpensesWithFilterQuery request, IQueryable<ExpenseMemento> query)
        {
            if (request.UserId.HasValue)
            {
                query = query.Where(w => w.UserId == request.UserId);
            }

            return query;
        }
    }
}