using Cleemy.Ressources;
using Cleemy.Services;
using Cleemy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cleemy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserExpensesController : ControllerBase
    {
        private readonly IUserExpenseService _userExpenseService;

        public UserExpensesController(IUserExpenseService userExpenseService)
        {
            _userExpenseService= userExpenseService;
        }

        /// <summary>
        /// Get all expenses registered for a given user
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="sortType">type of sorting</param>
        /// <returns>Expenses list</returns>
        [Route("GetUserExpensesList")]
        [HttpPost]
        public Task<ActionResult<IEnumerable<UserExpenseViewModel>>> GetUserExpensesList(int userId, UserExpenseViewModelSortType sortType) 
        {
            return _userExpenseService.GetUserExpensesList(userId, sortType);
        }

        /// <summary>
        /// Add a new expense
        /// </summary>
        /// <param name="expense">Expense details</param>
        /// <returns>Http status and message</returns>
        [Route("AddExpense")]
        [HttpPost]
        public async Task<IActionResult> AddExpense(ExpenseViewModel expense)
        {
            IEnumerable<string> expenseViewModelErrors = _userExpenseService.GetExpenseViewModelErrors(expense);
            if(expenseViewModelErrors.Any())
            {
                return BadRequest(expenseViewModelErrors);
            }

            try
            {
                if(await _userExpenseService.IsExpenseAlreadyExists(expense))
                {
                    return BadRequest(Messages.ExpenseAlredyExists);
                }
                await _userExpenseService.InsertExpense(expense);
            }
            catch (DbUpdateException)
            {
                return BadRequest(Messages.DataError);
            }

            return Ok();
        }
    }
}
