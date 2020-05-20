using ArnoAdminCore.Base.Services;
using ArnoAdminCore.SystemManage.Models.Poco;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Services
{
    public interface IUserService : IBaseCRUDService<User>
    {
        public User UpdateWithRole(User entity);
        public Task<IEnumerable<UserRole>> FindRoleByUserIdAsync(long id);
    }
}
