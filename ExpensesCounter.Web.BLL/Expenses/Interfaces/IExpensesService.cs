using System.Collections.Generic;
using System.Threading.Tasks;
using ExpensesCounter.Common.Models.Expenses;
using ExpensesCounter.Common.Models.Expenses.List;

namespace ExpensesCounter.Web.BLL.Expenses.Interfaces
{
    public interface IExpensesService
    {
        #region ExpensesList

        List<ExpensesListModel> GetExpensesLists(int offset, int pageSize);
        Task<ExpensesListDetailModel> GetExpensesListAsync(int listId);

        Task<bool> AddExpensesListAsync(CreateExpensesListModel createModel);
        Task<bool> UpdateExpensesListAsync(UpdateExpensesListModel updateModel);
        Task RemoveExpensesListAsync(int listId);
        Task<bool> UpdateAssignedUsersAsync(int listId, params int[] usersIds);

        #endregion

        #region Expenses

        Task<bool> AddExpenseAsync(AddExpenseModel addModel);
        Task<bool> UpdateExpenseAsync(UpdateExpenseModel updateModel);
        Task RemoveExpenseAsync(int expenseId);

        #endregion
    }
}