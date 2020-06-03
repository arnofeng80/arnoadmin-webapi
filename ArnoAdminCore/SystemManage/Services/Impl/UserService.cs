using ArnoAdminCore.Base.Constants;
using ArnoAdminCore.Base.Services.Impl;
using ArnoAdminCore.Cache;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.Web;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Services.Impl
{
    public class UserService : BaseCRUDService<User>, IUserService
    {
        private readonly IMapper _mapper;
        public UserService(UserRepository repo, IMapper mapper) : base(repo)
        {
            this._mapper = mapper;
        }
        public override User Update(User entity)
        {
            if (String.IsNullOrWhiteSpace(entity.Password))
            {
                Repository.DbContext.Entry<User>(entity).Property("Password").IsModified = false;
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

        public User FindUserByLoginName(string loginName)
        {
            return Repository.DbContext.Set<User>().Where(x => x.LoginName == loginName).FirstOrDefault();
        }

        public User FindUserByLoginName(string loginName, string password)
        {
            return Repository.DbContext.Set<User>().Where(x => x.LoginName == loginName && x.Password == password).FirstOrDefault();
        }

        public User FindUserByToken(string token)
        {
            return Repository.DbContext.Set<User>().Where(x => x.Token == token).FirstOrDefault();
        }

        public User Login(string userName, string password)
        {
            if(String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("用戶名或密碼不能為空");
            }

            User user = FindUserByLoginName(userName.Trim(), password.Trim());
            if(user == null)
            {
                throw new ArgumentException("用戶名或密碼錯誤");
            }

            if(user.Status == DictConst.NORMAL_DISABLE)
            {
                throw new ArgumentException("用戶已停用");
            }

            var prevToken = user.Token;
            user.Token = Guid.NewGuid().ToString().Replace("-", "");
            user.LoginIp = Current.IP;
            user.LoginDate = DateTime.Now;

            UpdateLoginInfo(user);
            Current.Session.SetString(WebConst.TOKEN_NAME, user.Token);
            if (!String.IsNullOrWhiteSpace(prevToken))
            {
                CacheFactory.Cache.RemoveCache(prevToken);
            }
            return user;
        }

        public User UpdateLoginInfo(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            //Repository.DbContext.Set<User>().Update(user);
            Repository.DbContext.SaveChanges();
            return user;
        }
    }
}
