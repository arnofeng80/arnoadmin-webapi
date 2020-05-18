using ArnoAdminCore.Base.Models;
using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Base.Services.Impl;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Services.Impl
{
    public class RoleService : BaseCRUDService<Role>, IRoleService
    {
       public RoleService(RoleRepository repo) : base(repo)
        {

        }
        public override void Delete(long id)
        {
            Repository.DbContext.Set<RoleMenu>().RemoveRange(Repository.DbContext.Set<RoleMenu>().Where(x => x.RoleId == id));
            Repository.DbContext.Set<RoleDept>().RemoveRange(Repository.DbContext.Set<RoleDept>().Where(x => x.RoleId == id));
            base.Delete(id);
        }
        public Role AddWithMenu(Role entity)
        {
            return base.Add(entity);
        }
        public Role UpdateWithMenu(Role entity)
        {
            using (var tran = this.Repository.BeginTransaction())
            {
                this.Repository.DbContext.Set<RoleMenu>().RemoveRange(this.Repository.DbContext.Set<RoleMenu>().Where(x => x.RoleId == entity.Id));
                this.Repository.Save();
                foreach (var roleMenu in entity.RoleMenus)
                {
                    roleMenu.RoleId = entity.Id;
                    this.Repository.DbContext.Set<RoleMenu>().Add(roleMenu);
                }

                base.Update(entity);
                Repository.Save();
                tran.Commit();
            }
            return entity;
        }
        public Role UpdateWithDept(Role entity)
        {
            using (var tran = this.Repository.BeginTransaction())
            {
                this.Repository.DbContext.Set<RoleDept>().RemoveRange(this.Repository.DbContext.Set<RoleDept>().Where(x => x.RoleId == entity.Id));
                this.Repository.Save();
                foreach (var roleDept in entity.RoleDepts)
                {
                    roleDept.RoleId = entity.Id;
                    this.Repository.DbContext.Set<RoleDept>().Add(roleDept);
                }
                base.Update(entity);
                Repository.Save();
                tran.Commit();
            }
            return entity;
        }
        public async Task<IEnumerable<RoleMenu>> FindMenusByRoleIdAsync(long id)
        {
            var list = from rm in _repo.DbContext.Set<RoleMenu>()
                       where rm.RoleId == id && !(
                            from srm in _repo.DbContext.Set<RoleMenu>()
                            join sm in _repo.DbContext.Set<Menu>() on srm.MenuId equals sm.Id
                            where srm.RoleId == id
                            select sm.ParentId
                       ).Contains(rm.MenuId)
                       select rm;
            return await list.ToListAsync();
        }

        public async Task<IEnumerable<RoleDept>> FindDeptByRoleIdAsync(long id)
        {
            var list = from rd in _repo.DbContext.Set<RoleDept>()
                       where rd.RoleId == id && !(
                            from srd in _repo.DbContext.Set<RoleDept>()
                            join sd in _repo.DbContext.Set<Department>() on srd.DeptId equals sd.Id
                            where srd.RoleId == id
                            select sd.ParentId
                       ).Contains(rd.DeptId)
                       select rd;
            return await list.ToListAsync();
        }
    }
}
