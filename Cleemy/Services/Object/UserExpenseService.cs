using Cleemy.Entities;
using Cleemy.Models;
using Cleemy.Ressources;
using Cleemy.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cleemy.Services.Object
{
    public class UserExpenseService : IUserExpenseService
    {
        private readonly IUserExpenseModel _userExpenseModel;

        public UserExpenseService(IUserExpenseModel userExpenseModel)
        {
            _userExpenseModel = userExpenseModel;
        }

        public Task<ActionResult<IEnumerable<UserExpenseViewModel>>> GetUserExpensesList(int userID, UserExpenseViewModelSortType sortType)
        {
            return _userExpenseModel.GetUserExpensesList(userID, sortType);
        }

        public async Task<int> InsertExpense(ExpenseViewModel expense)
        {
            return await _userExpenseModel.InsertExpense(
                new Expense()
                {
                    FkCleemyUser = expense.UserId,
                    FkExpenseNature = expense.NatureId,
                    Amount = expense.Amount,
                    Commentary = expense.Commentary,
                    ExpenseDate = expense.Date
                });
        }

        public IEnumerable<string> GetExpenseViewModelErrors(ExpenseViewModel expense)
        {
            if (expense.Date.Date >= DateTime.Now.AddDays(1).Date)
            {
                yield return Messages.DateAboveToday;
            }

            if (expense.Date.Date < DateTime.Now.AddMonths(-3).Date)
            {
                yield return Messages.DateThreeMonthOld;
            }

            if (string.IsNullOrEmpty(expense.Commentary))
            {
                yield return Messages.EmptyCommentary;
            }
        }

        public async Task<bool> IsExpenseAlreadyExists(ExpenseViewModel expenseViewModel)
        {
            var expenses = await _userExpenseModel.GetUserExpenses(expenseViewModel.UserId);
            foreach(Expense expense in expenses)
            {
                if (expense.Amount == expenseViewModel.Amount
                    && expense.ExpenseDate.Date == expenseViewModel.Date.Date)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
