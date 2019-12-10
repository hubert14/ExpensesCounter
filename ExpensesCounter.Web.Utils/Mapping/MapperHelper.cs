using AutoMapper;

namespace ExpensesCounter.Web.Utils.Mapping
{
    public static class MapperHelper
    {
        public static IMapper ConfiguredMapper =>
            new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new UsersMappingProfile());
                    mc.AddProfile(new ExpensesMappingProfile());
                }).CreateMapper();
    }
}