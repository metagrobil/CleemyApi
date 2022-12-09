namespace Cleemy.ViewModels
{
    public class ExpenseViewModel
    {
        public int UserId { get; set; }
        public int NatureId { get; set; }
        public decimal Amount { get; set; }
        public string Commentary { get; set; }
        public DateTime Date { get; set; }
    }
}
