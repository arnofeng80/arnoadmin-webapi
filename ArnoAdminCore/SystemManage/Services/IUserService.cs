using ArnoAdminCore.Base.Services;
using ArnoAdminCore.SystemManage.Models.Dto.List;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Services
{
    public interface IUserService : IBaseCRUDService<User>
    {
        public User UpdateWithRole(User entity);
        //public Task<IEnumerable<UserRole>> FindRoleByUserIdAsync(long id);
        public User Login(string userName, string password);
        public void Logout();
        public User FindByLoginName(string loginName, string password);
        public User FindByLoginName(string loginName);
        public User FindByToken(string loginName);
        public List<Role> FindRolesByUserId(long id);
        public List<Menu> FindMenusByUserId(long id);
        public List<Menu> FindPermissionsByUserId(long id);
        public IEnumerable<MenuList> FindMenuTreeByUserId(long userId);
    }
}
