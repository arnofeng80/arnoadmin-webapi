using ArnoAdminCore.Base.Services;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Services
{
    public interface IUserService : IBaseCRUDService<User>
    {
        public User UpdateWithRole(User entity);
        public Task<IEnumerable<UserRole>> FindRoleByUserIdAsync(long id);
        public User Login(string userName, string password);
        public User UpdateLoginInfo(User user);
        public User FindUserByLoginName(string loginName, string password);
        public User FindUserByLoginName(string loginName);
        public User FindUserByToken(string loginName);
    }
}
