using System.Threading.Tasks;
using ExpensesCounter.Common.Models.User;

namespace ExpensesCounter.Web.BLL.Account.Interfaces
{
    public interface IAccountService
    {
        Task<UserInfoModel> GetInfoAsync(int userId);
        Task UpdateInfoAsync(int userId, UpdateUserInfoModel updateModel);
        Task ChangeEnableStateAsync(int userId);
    }
}