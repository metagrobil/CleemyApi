using Cleemy.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cleemy.Services
{
    public interface IUserExpenseService
    {
        Task<ActionResult<IEnumerable<UserExpenseViewModel>>> GetUserExpensesList(int userId, UserExpenseViewModelSortType sortType);
        Task<int> InsertExpense(ExpenseViewModel expense);
        IEnumerable<string> GetExpenseViewModelErrors(ExpenseViewModel expense);
        Task<bool> IsExpenseAlreadyExists(ExpenseViewModel expense);
    }
}
