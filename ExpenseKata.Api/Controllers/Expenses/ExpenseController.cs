using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ExpenseKata.Application.Expenses.Command.CreateExpense;
using ExpenseKata.Application.Expenses.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseKata.Api.Controllers.Expenses
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExpenseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<ActionResult> CreateExpense(CreateExpenseCommand command)
        {
            await _mediator.Send(command);

            return Accepted();
        }

        [HttpGet("expenses")]
        [ProducesResponseType(typeof(List<ExpenseDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromQuery]ExpenseListQuery query)
        {
            return Ok(await _mediator.Send(new GetExpensesWithFilterQuery { UserId = query.UserId, OrderBy = query.OrderBy}));
        }
    }
}
