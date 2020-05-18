using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArnoAdminCore.SystemManage.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SystemDbContext context) : base(context) { }


        //public override User Add(User entity)
        //{
        //    base.Add(entity);
        //    if(entity.RoleIds != null && entity.RoleIds.Length > 0)
        //    {
        //        this._context.Set<UserRole>().AddRange(entity.RoleIds.Select(x => new UserRole { UserId = entity.Id, RoleId = x }));
        //    }
        //    return entity;
        //}
        //public override User Update(User entity)
        //{
        //    this._context.Set<UserRole>().RemoveRange(this._context.Set<UserRole>().Where(x => x.UserId == entity.Id));
        //    this._context.SaveChanges();
        //    if (entity.RoleIds != null && entity.RoleIds.Length > 0)
        //    {
        //        this._context.Set<UserRole>().AddRange(entity.RoleIds.Select(x => new UserRole { UserId = entity.Id, RoleId = x }));
        //    }

        //    base.Update(entity);
        //    return entity;
        //}
    }
}
