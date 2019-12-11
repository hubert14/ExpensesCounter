using AutoMapper;
using ExpensesCounter.Common.Models.Expenses;
using ExpensesCounter.Common.Models.Expenses.List;
using ExpensesCounter.Web.DAL.Entities;

namespace ExpensesCounter.Web.Utils.Mapping
{
    internal class ExpensesMappingProfile : Profile
    {
        public ExpensesMappingProfile()
        {
            CreateMap<Expense, ExpensesModel>();

            CreateMap<Expense, ExpensesDetailModel>()
                .ForMember(x => x.ListId,
                    e => e.MapFrom(x => x.ExpenseListId))
                .ForMember(x => x.ListTitle,
                    e => e.MapFrom(x => x.ExpenseList.Title))
                .ForMember(x => x.ListComment,
                    e => e.MapFrom(x => x.ExpenseList.Comment));

            CreateMap<AddExpenseModel, Expense>()
                .ForMember(x => x.ExpenseListId,
                    e => e.MapFrom(x => x.ListId));

            CreateMap<ExpensesList, ExpensesListModel>();
            CreateMap<ExpensesList, ExpensesListDetailModel>()
                .ForMember(x => x.Expenses, e => e.MapFrom(x => x.Expenses));
            CreateMap<CreateExpensesListModel, ExpensesList>();
        }
    }
}