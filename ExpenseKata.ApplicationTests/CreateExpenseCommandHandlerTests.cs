using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ExpenseKata.Application.Expenses.Command.CreateExpense;
using ExpenseKata.Domain.Common;
using ExpenseKata.Domain.Expenses;
using ExpenseKata.Domain.Users;
using ExpenseKata.Infrastructure.Interfaces;
using NSubstitute;
using Xunit;

namespace ExpenseKata.ApplicationTests
{
    public class CreateExpenseCommandHandlerTests
    {
        [Fact]
        public async void HandleCommandCreateNewExpense()
        {
            var userId = Guid.NewGuid();
            var uof = Substitute.For<IUnitOfWork>();
            var dateProvider = Substitute.For<IDateTimeProvider>();
            dateProvider.Now.Returns(new DateTime(2021, 05, 01));
            var expenseRepo = new ExpenseStubRepo();
            var userRepo = Substitute.For<IUserRepository>();
            userRepo.GetByIdAsync(userId).Returns(new User
                {Currency = Currency.Dollar, LastName = "Jonh", FirstName = "Doe"});

            var command = new CreateExpenseCommand()
            {
                Currency = "Dollar",
                Amount = 15,
                Comment = "Commentaire",
                ExpenseDate = new DateTime(2021, 04, 30),
                ExpenseNature = "Hotel",
                UserId = userId
            };
            var sut = new CreateExpenseCommandHandler(uof, dateProvider, expenseRepo, userRepo);
            await sut.Handle(command, new CancellationToken(false));

            Assert.Equal(1, expenseRepo.AddedExpenses.Count);
        }
    }

    internal class ExpenseStubRepo : IExpenseRepository
    {
        public ExpenseStubRepo()
        {
            AddedExpenses = new List<Expense>();
        }

        public IList<Expense> AddedExpenses { get; set; }
        public Task AddAsync(Expense expense)
        {
            AddedExpenses.Add(expense);
            return Task.CompletedTask;
        }

        public Task<Expense> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserExpenseHistory>> GetExpenseHistoryForUser(Guid requestUserId)
        {
            return Task.FromResult(new List<UserExpenseHistory>().AsEnumerable());
        }
    }
}
