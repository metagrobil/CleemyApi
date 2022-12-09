using Cleemy.Context;
using Cleemy.Entities;
using Cleemy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cleemy.Models.Object
{
    public class UserExpenseModel : IUserExpenseModel
    {
        private readonly CleemyBDContext _context;

        public UserExpenseModel(CleemyBDContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<UserExpenseViewModel>>> GetUserExpensesList(int userId, UserExpenseViewModelSortType sortType = 0)
        {
            List<Expense> expenses = await GetUserExpenses(userId);

            var userExpenseViewModel = new List<UserExpenseViewModel>();
            foreach (var expense in expenses)
            {
                userExpenseViewModel.Add(new UserExpenseViewModel(expense));
            }

            if (sortType == UserExpenseViewModelSortType.Amount)
            {
                userExpenseViewModel = userExpenseViewModel.OrderBy(e => e.ExpenseAmount).ToList();
            }
            if (sortType == UserExpenseViewModelSortType.Date)
            {
                userExpenseViewModel = userExpenseViewModel.OrderBy(e => e.ExpenseDate).ToList();
            }

            return userExpenseViewModel;
        }

        public async Task<List<Expense>> GetUserExpenses(int userId)
        {
            return await _context.Expense
                .Where(e => e.FkCleemyUser == userId)
                .Include(e => e.FkCleemyUserNavigation)
                .Include(e => e.FkExpenseNatureNavigation)
                .ToListAsync();
        }

        public async Task<int> InsertExpense(Expense expense)
        {
            _context.Entry(expense).State = EntityState.Added;
            return await _context.SaveChangesAsync();
        }
    }
}
