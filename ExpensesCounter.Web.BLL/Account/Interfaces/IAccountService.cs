using System.Threading.Tasks;
using ExpensesCounter.Common.Models.User;

namespace ExpensesCounter.Web.BLL.Account.Interfaces
{
    public interface IAccountService
    {
        Task<UserInfoModel> GetInfoAsync();
        Task<bool> UpdateInfoAsync(UpdateUserInfoModel updateModel);
        Task<bool> ChangeEnableStateAsync();
    }
}