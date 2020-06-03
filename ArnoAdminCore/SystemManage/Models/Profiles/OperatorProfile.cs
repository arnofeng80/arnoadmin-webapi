using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.Web;
using AutoMapper;

namespace ArnoAdminCore.SystemManage.Models.Profiles
{
    public class OperatorProfile : Profile
    {
        public OperatorProfile()
        {
            CreateMap<User, Operator>();
        }
    }
}
