using System.Collections.Generic;

namespace ExpensesCounter.Common.Models.Expenses.List
{
    public class ExpensesListDetailModel : ExpensesListModel
    {
        public ICollection<ExpensesModel> Expenses { get; set; }
    }
}