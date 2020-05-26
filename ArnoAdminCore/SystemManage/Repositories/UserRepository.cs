using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;

namespace ArnoAdminCore.SystemManage.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SystemDbContext context) : base(context) { }
    }
}
