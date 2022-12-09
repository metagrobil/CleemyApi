using Cleemy.Models;
using Cleemy.Services.Object;
using Cleemy.ViewModels;
using Moq;

namespace CleemyTest
{
    public class GetExpenseViewModelErrors
    {
        private Mock<IUserExpenseModel> _userExpenseModelMock;
        private UserExpenseService _userExpenseService;

        public GetExpenseViewModelErrors()
        {
            _userExpenseModelMock = new Mock<IUserExpenseModel>();
            _userExpenseService = new UserExpenseService(_userExpenseModelMock.Object);
        }

        [Fact]
        public void ItExists()
        {
            // Arrange
            var expense = new ExpenseViewModel();

            // Act && Assert
            IEnumerable<string> result = _userExpenseService.GetExpenseViewModelErrors(expense);
        }


        [Fact]
        public void GivenACorrectViewModel_ShouldReturnEmptyList()
        {
            // Arrange
            var expense = new ExpenseViewModel()
            {
                Date = DateTime.Now,
                Commentary = "com",
                Amount = 0,
            };

            // Act
            IEnumerable<string> result = _userExpenseService.GetExpenseViewModelErrors(expense);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GivenAFuturDateExpense_ShouldReturnAnError()
        {
            // Arrange
            var expense = new ExpenseViewModel()
            {
                Date = DateTime.Now.AddDays(1),
                Commentary = "com",
                Amount= 0,
            };

            // Act
            IEnumerable<string> result = _userExpenseService.GetExpenseViewModelErrors(expense);

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void GivenA3MonthPastDate_ShouldReturnAnError()
        {
            // Arrange
            var expense = new ExpenseViewModel()
            {
                Date = DateTime.Now.AddMonths(-3).AddDays(-1),
                Commentary = "com",
                Amount = 0,
            };

            // Act
            IEnumerable<string> result = _userExpenseService.GetExpenseViewModelErrors(expense);

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }

        [Fact]
        public void GivenA3MonthDate_ShouldReturnEmptyList()
        {
            // Arrange
            var expense = new ExpenseViewModel()
            {
                Date = DateTime.Now.AddMonths(-3),
                Commentary = "com",
                Amount = 0,
            };

            // Act
            IEnumerable<string> result = _userExpenseService.GetExpenseViewModelErrors(expense);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GivenAnEmptyCommentary_ShouldReturnAnError()
        {
            // Arrange
            var expense = new ExpenseViewModel()
            {
                Date = DateTime.Now,
                Commentary = "",
                Amount = 0,
            };

            // Act
            IEnumerable<string> result = _userExpenseService.GetExpenseViewModelErrors(expense);

            // Assert
            Assert.NotEmpty(result);
            Assert.Single(result);
        }
    }
}