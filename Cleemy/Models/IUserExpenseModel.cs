using Cleemy.Entities;
using Cleemy.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cleemy.Models
{
    public interface IUserExpenseModel
    {
        Task<ActionResult<IEnumerable<UserExpenseViewModel>>> GetUserExpensesList(int userId, UserExpenseViewModelSortType sortType = 0);
        Task<List<Expense>> GetUserExpenses(int userId);
        Task<int> InsertExpense(Expense expense);
    }
}
