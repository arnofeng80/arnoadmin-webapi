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
    public class UserService : BaseCRUDService<User>, IUserService
    {
        public UserService(UserRepository repo) : base(repo)
        {

        }
        public override User Update(User entity)
        {
            if (String.IsNullOrWhiteSpace(entity.Password))
            {
                Repository.DbContext.Entry<User>(entity).Property("Password").IsModified = false;
            } else
            {
                entity.Password = entity.Password.Trim();
            }
            return base.UpdatePartial(entity);
        }
        public override void Delete(long id)
        {
            Repository.DbContext.Set<UserRole>().RemoveRange(Repository.DbContext.Set<UserRole>().Where(x => x.UserId == id));
            base.Delete(id);
        }
        public User UpdateWithRole(User entity)
        {
            using (var tran = this.Repository.BeginTransaction())
            {
                this.Repository.DbContext.Set<UserRole>().RemoveRange(this.Repository.DbContext.Set<UserRole>().Where(x => x.UserId == entity.Id));
                this.Repository.Save();
                this.Repository.DbContext.Attach(entity);
                this.Repository.DbContext.Entry<User>(entity).State = EntityState.Modified;
                foreach (var userRole in entity.UserRoles)
                {
                    userRole.UserId = entity.Id;
                    this.Repository.DbContext.Set<UserRole>().Add(userRole);
                }

                Update(entity);
                tran.Commit();
            }
            return entity;
        }
        public async Task<IEnumerable<UserRole>> FindRoleByUserIdAsync(long id)
        {
            return await this.Repository.DbContext.Set<UserRole>().Where(x => x.UserId == id).ToListAsync();
        }
    }
}
