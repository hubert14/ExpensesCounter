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
        private readonly IMapper _mapper;
        private readonly CurrentUserInfo _currentUser;

        public AccountService(ApplicationContext context, IMapper mapper, CurrentUserInfo currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<UserInfoModel> GetInfoAsync()
        {
            var user = await _context.Users.FindAsync(_currentUser.Id);
            if (user == null) return null;

            var mapped = _mapper.Map<UserInfoModel>(user);
            return mapped;
        }

        public async Task<bool> UpdateInfoAsync(UpdateUserInfoModel updateModel)
        {
            var user = await _context.Users.FindAsync(_currentUser.Id);
            if (user == null) return false;

            user.Email = updateModel.Email;
            user.Birthday = updateModel.Birthday;
            user.PhoneNumber = updateModel.PhoneNumber;
            user.FirstName = updateModel.FirstName;
            user.LastName = updateModel.LastName;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeEnableStateAsync()
        {
            var user = await _context.Users.FindAsync(_currentUser.Id);
            if (user == null) return false;

            user.IsEnabled = !user.IsEnabled;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}