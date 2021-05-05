using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Users;
using ExpenseKata.Infrastructure.Interfaces;
using MediatR;

namespace ExpenseKata.Application.Expenses.Command.CreateExpense
{
    public class CreateExpenseCommand : IRequest
    {
        public Guid UserId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; }

        public string Comment { get; set; }

        public DateTime ExpenseDate { get;set; }

        public string ExpenseNature { get; set; }
    }

    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateProvider;
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserRepository _userRepository;

        public CreateExpenseCommandHandler(IUnitOfWork unitOfWork, IDateTimeProvider dateProvider, IExpenseRepository expenseRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _dateProvider = dateProvider;
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(CreateExpenseCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if(user == null)
            {
                throw new ApplicationException($"L'utilisateur est inconnu");
            }
            IEnumerable<UserExpenseHistory> history = await _expenseRepository.GetExpenseHistoryForUser(command.UserId);

            var expense = Expense.Create(_dateProvider, 
                        command.Comment, 
                        command.ExpenseDate, 
                        new ExpenseUser(user.Id, user.Currency, history), 
                        command.Currency, 
                        command.Amount,
                        command.ExpenseNature);

            await _expenseRepository.AddAsync(expense);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}