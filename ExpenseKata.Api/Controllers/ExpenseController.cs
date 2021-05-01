using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ExpenseKata.Application.Expenses.Command.CreateExpense;
using MediatR;

namespace ExpenseKata.Api.Controllers
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
        public async Task<ActionResult> CreateExpense(CreateExpenseModel model)
        {
            //var anthonySparkUser = Guid.Parse("e4193184-c35c-4dec-9748-7407d788dc52");
            await _mediator.Send(new CreateExpenseCommand()
            {
                Currency = model.Currency,
                Amount = model.Amount,
                Comment = model.Comment,
                ExpenseDate = model.ExpenseDate,
                UserId = model.UserId,
                ExpenseNature = model.ExpenseNature
            });

            return Accepted();
        }
    }

    public class CreateExpenseModel
    {
        public Guid UserId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }

        public DateTime ExpenseDate { get;set; }

        public string ExpenseNature { get; set; }
    }
}
