using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpensesCounter.Common.Models.Expenses;
using ExpensesCounter.Common.Models.Expenses.List;
using ExpensesCounter.Web.BLL.Expenses.Interfaces;
using ExpensesCounter.Web.BLL.Extensions;
using ExpensesCounter.Web.DAL;
using ExpensesCounter.Web.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpensesCounter.Web.BLL.Expenses.Services
{
    internal class ExpensesService : IExpensesService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;
        private readonly CurrentUserInfo _currentUserInfo;

        public ExpensesService(ApplicationContext context, IMapper mapper, CurrentUserInfo currentUserInfo)
        {
            _context = context;
            _mapper = mapper;
            _currentUserInfo = currentUserInfo;
        }

        public async Task<List<ExpensesListModel>> GetExpensesListsAsync(int offset, int pageSize)
        {
            var userLists = await _context.UserLists
                                          .Where(listUser => listUser.UserId == _currentUserInfo.Id &&
                                                             listUser.IsActiveAssign)
                                          .Select(listUser => listUser.ExpensesList)
                                          .MakeOffset(offset, pageSize)
                                          .ToListAsync();

            var mapped = _mapper.Map<List<ExpensesListModel>>(userLists);
            return mapped;
        }

        public async Task<ExpensesListDetailModel> GetExpensesListAsync(int listId)
        {
            var expensesList = await _context.ExpensesLists
                                             .Include(x => x.Expenses)
                                             .FirstOrDefaultAsync(list =>
                                                 list.Id == listId &&
                                                 list.Users
                                                     .Select(listUser => listUser.UserId)
                                                     .Contains(_currentUserInfo.Id));

            if (expensesList == null) return null;

            var mapped = _mapper.Map<ExpensesListDetailModel>(expensesList);
            return mapped;
        }

        public async Task<bool> AddExpensesListAsync(CreateExpensesListModel createModel)
        {
            var newList = _mapper.Map<ExpensesList>(createModel);

            _context.UserLists.Add(new ExpensesListUser {UserId = _currentUserInfo.Id, ExpensesList = newList, IsActiveAssign = true});
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateExpensesListAsync(int listId, UpdateExpensesListModel updateModel)
        {
            var entity = await _context.ExpensesLists
                                       .FirstOrDefaultAsync(list =>
                                           list.Id == listId &&
                                           list.Users.Select(u => u.UserId).Contains(_currentUserInfo.Id));

            entity.Title = updateModel.Title;
            entity.Comment = updateModel.Comment;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task RemoveExpensesListAsync(int listId)
        {
            var entity = await _context.ExpensesLists
                                       .FirstOrDefaultAsync(list =>
                                           list.Id == listId &&
                                           list.Users.Select(u => u.UserId).Contains(_currentUserInfo.Id));

            entity.IsDeleted = true;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAssignedUsersAsync(int listId, params int[] usersIds)
        {
            var existedUsers = await _context.UserLists
                                             .Where(listUser => listUser.ExpenseListId == listId)
                                             .ToListAsync();

            var existedUsersIds = existedUsers.Select(r => r.UserId).ToList();

            var toCreate = usersIds.Except(existedUsersIds);
            var toRemove = existedUsersIds.Except(usersIds);

            foreach (var userId in toCreate) _context.UserLists.Add(new ExpensesListUser(userId, listId));
            foreach (var userId in toRemove) _context.UserLists.Remove(existedUsers.First(i => i.UserId == userId));

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddExpenseAsync(AddExpenseModel addModel)
        {
            var newExpense = _mapper.Map<Expense>(addModel);

            _context.Expenses.Add(newExpense);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateExpenseAsync(int id, UpdateExpenseModel updateModel)
        {
            var entity = await _context.Expenses.FindAsync(id);
            if (entity == null) return false;

            entity.Comment = updateModel.Comment;
            entity.Count = updateModel.Count;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task RemoveExpenseAsync(int expenseId)
        {
            var entity = await _context.Expenses
                                       .FirstOrDefaultAsync(expense =>
                                           expense.Id == expenseId &&
                                           expense.ExpenseList.Users
                                                  .Select(u => u.UserId)
                                                  .Contains(_currentUserInfo.Id));

            if (entity == null) return;

            entity.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
    }
}