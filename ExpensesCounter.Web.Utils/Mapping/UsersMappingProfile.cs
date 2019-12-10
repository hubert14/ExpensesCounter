using AutoMapper;
using ExpensesCounter.Common.Models.Auth;
using ExpensesCounter.Common.Models.User;
using ExpensesCounter.Web.DAL.Entities;

namespace ExpensesCounter.Web.Utils.Mapping
{
    internal class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<User, UserInfoModel>();
            CreateMap<RegisterModel, User>();
        }
    }
}
