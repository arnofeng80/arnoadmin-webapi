using ArnoAdminCore.Base.Repository;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Repository
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(SystemDbContext context) : base(context) { }

        public override Role Add(Role entity)
        {
            base.Add(entity);
            foreach (var roleMenu in entity.RoleMenus)
            {
                roleMenu.RoleId = entity.Id;
                this._context.Set<RoleMenu>().Add(roleMenu);
            }
            return entity;
        }
        public override Role Update(Role entity)
        {
            this._context.Set<RoleMenu>().RemoveRange(this._context.Set<RoleMenu>().Where(x => x.RoleId == entity.Id));
            this._context.SaveChanges();
            foreach (var roleMenu in entity.RoleMenus)
            {
                roleMenu.RoleId = entity.Id;
                this._context.Set<RoleMenu>().Add(roleMenu);
            }

            base.Update(entity);
            return entity;
        }

        public async Task<IEnumerable<RoleMenu>> FindMenusByRoleIdAsync(long id)
        {
            return await _context.Set<RoleMenu>().Where(x => x.RoleId == id).ToListAsync();
        }
    }
}
