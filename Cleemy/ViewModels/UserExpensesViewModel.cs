using Cleemy.Entities;

namespace Cleemy.ViewModels
{
    public class UserExpenseViewModel
    {
        public string UserFirstNameLastName { get; set; }
        public decimal ExpenseAmount { get; set; }
        public string UserCurrency { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseNature { get; set; }
        public string ExpenseCommentary { get; set; }

        public UserExpenseViewModel(Expense expense)
        {
            UserFirstNameLastName = expense.FkCleemyUserNavigation.FirstName + " " + expense.FkCleemyUserNavigation.LastName;
            ExpenseDate = expense.ExpenseDate;
            ExpenseAmount = expense.Amount;
            UserCurrency = expense.FkCleemyUserNavigation.Currency;
            ExpenseNature = expense.FkExpenseNatureNavigation.Name;
            ExpenseCommentary = expense.Commentary;
        }
    }

    public enum UserExpenseViewModelSortType
    {
        Amount,
        Date,
    }
}
