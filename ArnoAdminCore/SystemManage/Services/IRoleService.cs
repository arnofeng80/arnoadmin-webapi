using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Base.Services;
using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Services
{
    public interface IRoleService: IBaseCRUDService<Role>
    {
        public Role AddWithMenu(Role entity);
        public Role UpdateWithMenu(Role entity);
        public Role UpdateWithDept(Role entity);
        public Task<IEnumerable<RoleMenu>> FindMenusByRoleIdAsync(long id);
        public Task<IEnumerable<RoleDept>> FindDeptByRoleIdAsync(long id);
    }
}
