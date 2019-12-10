using System.Threading.Tasks;
using AutoMapper;
using ExpensesCounter.Common.Models.User;
using ExpensesCounter.Web.BLL.Account.Interfaces;
using ExpensesCounter.Web.DAL;

namespace ExpensesCounter.Web.BLL.Account.Services
{
    internal class AccountService : IAccountService
    {
        private readonly ApplicationContext _context;
        private readonly IMapper            _mapper;

        public AccountService(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _mapper  = mapper;
        }

        public async Task<UserInfoModel> GetInfoAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            var mapped = _mapper.Map<UserInfoModel>(user);
            return mapped;
        }

        public async Task UpdateInfoAsync(int userId, UpdateUserInfoModel updateModel)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            user.Email       = updateModel.Email;
            user.Birthday    = updateModel.Birthday;
            user.PhoneNumber = updateModel.PhoneNumber;
            user.FirstName   = updateModel.FirstName;
            user.LastName    = updateModel.LastName;

            await _context.SaveChangesAsync();
        }

        public async Task ChangeEnableStateAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return;

            user.IsEnabled = !user.IsEnabled;

            await _context.SaveChangesAsync();
        }
    }
}