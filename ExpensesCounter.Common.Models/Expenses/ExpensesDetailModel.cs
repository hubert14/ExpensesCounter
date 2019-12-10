namespace ExpensesCounter.Common.Models.Expenses
{
    public class ExpensesDetailModel : ExpensesModel
    {
        public int ListId { get; set; }
        public string ListTitle { get; set; }
        public string ListComment { get; set; }
    }
}