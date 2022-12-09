using Cleemy.Entities;
using Cleemy.Models;
using Cleemy.Services.Object;
using Cleemy.ViewModels;
using Moq;

namespace CleemyTest
{
    public class IsExpenseAlreadyExists
    {
        private Mock<IUserExpenseModel> _userExpenseModelMock;
        private UserExpenseService _userExpenseService;
        public IsExpenseAlreadyExists()
        {
            _userExpenseModelMock = new Mock<IUserExpenseModel>();
            _userExpenseService = new UserExpenseService(_userExpenseModelMock.Object);
        }

        [Fact]
        public void ItExists()
        {
            // Arrange
            var givenExpense = new ExpenseViewModel()
            {
                UserId = 1,
            };

            var storedExpenses = new List<Expense>();

            _userExpenseModelMock.Setup(u => u.GetUserExpenses(1)).Returns(Task.FromResult(storedExpenses));

            // Act && Assert
            bool result = _userExpenseService.IsExpenseAlreadyExists(givenExpense).Result;
        }

        [Fact]
        public void GivenTheSameExpense_ShouldReturnTrue()
        {
            // Arrange
            var currentDate = DateTime.Now;
            var givenExpense = new ExpenseViewModel()
            {
                UserId = 1,
                Amount = 0,
                Date = currentDate,
            };

            var storedExpenses = new List<Expense>();
            storedExpenses.Add(new Expense()
            {
                Amount = 0,
                ExpenseDate = currentDate,
            });

            _userExpenseModelMock.Setup(u => u.GetUserExpenses(1)).Returns(Task.FromResult(storedExpenses));
            
            // Act
            bool result = _userExpenseService.IsExpenseAlreadyExists(givenExpense).Result;

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void GivenADifferentExpense_ShouldReturnTrue()
        {
            // Arrange
            var currentDate = DateTime.Now;
            var givenExpense = new ExpenseViewModel()
            {
                UserId = 1,
                Amount = 0,
                Date = currentDate,
            };

            var storedExpenses = new List<Expense>();
            storedExpenses.Add(new Expense()
            {
                Amount = 1,
                ExpenseDate = currentDate,
            });

            _userExpenseModelMock.Setup(u => u.GetUserExpenses(1)).Returns(Task.FromResult(storedExpenses));

            // Act
            bool result = _userExpenseService.IsExpenseAlreadyExists(givenExpense).Result;

            // Assert
            Assert.False(result);
        }
    }
}
