using System.Collections.Generic;
using System.Threading.Tasks;
using ExpensesCounter.Common.Models.Expenses;
using ExpensesCounter.Common.Models.Expenses.List;

namespace ExpensesCounter.Web.BLL.Expenses.Interfaces
{
    public interface IExpensesService
    {
        #region ExpensesList

        Task<List<ExpensesListModel>> GetExpensesListsAsync(int offset, int pageSize);
        Task<ExpensesListDetailModel> GetExpensesListAsync(int listId);

        Task<bool> AddExpensesListAsync(CreateExpensesListModel createModel);
        Task<bool> UpdateExpensesListAsync(int listId, UpdateExpensesListModel updateModel);
        Task RemoveExpensesListAsync(int listId);
        Task<bool> UpdateAssignedUsersAsync(int listId, params int[] usersIds);

        #endregion

        #region Expenses

        Task<bool> AddExpenseAsync(AddExpenseModel addModel);
        Task<bool> UpdateExpenseAsync(int id, UpdateExpenseModel updateModel);
        Task RemoveExpenseAsync(int expenseId);

        #endregion
    }
}