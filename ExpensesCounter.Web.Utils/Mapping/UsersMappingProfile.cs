using System;
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

            CreateMap<RegisterModel, User>()
                .ForMember(x => x.RegisterDateUtc, e => e.MapFrom(_ => DateTimeOffset.UtcNow))
                .ForMember(x => x.IsEnabled, e => e.MapFrom(_ => true)); // TODO: Change this after complete confirm user by email or phone number
        }
    }
}
