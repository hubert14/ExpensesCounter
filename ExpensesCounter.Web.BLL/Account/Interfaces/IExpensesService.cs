using System.Collections.Generic;
using System.Threading.Tasks;
using ExpensesCounter.Common.Models.Expenses;
using ExpensesCounter.Common.Models.Expenses.List;

namespace ExpensesCounter.Web.BLL.Account.Interfaces
{
    public interface IExpensesService
    {
        #region ExpensesList

        Task<List<ExpensesListModel>> GetExpensesListsAsync(int offset, int pageSize);
        Task<ExpensesListDetailModel> GetExpensesListAsync(int listId);

        bool AddExpensesList(int userId, CreateExpensesListModel createModel);
        Task<bool> UpdateExpensesListAsync(UpdateExpensesListModel updateModel);
        Task RemoveExpensesListAsync(int listId);
        Task<bool> UpdateAssignedUsersAsync(int ownerId, int listId, params int[] usersIds);

        #endregion

        #region Expenses

        bool AddExpense(AddExpenseModel addModel);
        Task<bool> UpdateExpenseAsync(UpdateExpenseModel updateModel);
        Task RemoveExpenseAsync(int expenseId);

        #endregion
    }
}