using ArnoAdminCore.Base.Constants;
using ArnoAdminCore.Base.Services.Impl;
using ArnoAdminCore.Cache;
using ArnoAdminCore.SystemManage.Models.Dto.List;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.Utils;
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
        [CacheEvict(Constants.USER_MENU)]
        public override void Delete(long id)
        {
            Repository.DbContext.Set<UserRole>().RemoveRange(Repository.DbContext.Set<UserRole>().Where(x => x.UserId == id));
            base.Delete(id);
        }
        [CacheEvict(Constants.USER_MENU)]
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
        //public async Task<IEnumerable<UserRole>> FindRoleByUserIdAsync(long id)
        //{
        //    return await this.Repository.DbContext.Set<UserRole>().Where(x => x.UserId == id).ToListAsync();
        //}

        public User FindByLoginName(string loginName)
        {
            return Repository.DbContext.Set<User>().Where(x => x.LoginName == loginName).FirstOrDefault();
        }

        public User FindByLoginName(string loginName, string password)
        {
            return Repository.DbContext.Set<User>().Where(x => x.LoginName == loginName && x.Password == password).FirstOrDefault();
        }

        public User FindByToken(string token)
        {
            return Repository.DbContext.Set<User>().Where(x => x.Token == token).FirstOrDefault();
        }

        public User Login(string userName, string password)
        {
            if(String.IsNullOrWhiteSpace(userName) || String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("用戶名或密碼不能為空");
            }

            User user = FindByLoginName(userName.Trim(), password.Trim());
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

            UpdateLoginOrLogout(user);
            Current.Session.SetString(WebConst.TOKEN_NAME, user.Token);
            if (!String.IsNullOrWhiteSpace(prevToken))
            {
                CacheFactory.Cache.RemoveCache(prevToken);
            }
            return user;
        }

        public void Logout()
        {
            var op = Current.Operator;
            if (op != null)
            {
                var user = FindById(op.Id);
                user.Token = "";
                UpdateLoginOrLogout(user);
                Current.Session.Remove(WebConst.TOKEN_NAME);
                Current.Session.Remove(WebConst.TOKEN_NAME);
                Current.Operator = null;
            }
        }

        private User UpdateLoginOrLogout(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            Repository.DbContext.SaveChanges();
            return user;
        }

        [Cacheable(Cache.Constants.USER_MENU)]
        public IEnumerable<MenuList> FindMenuTreeByUserId(long userId)
        {
            var userRoles = Repository.DbContext.Set<UserRole>().Where(x => x.UserId == userId).Select(x => x.RoleId).ToArray();
            var userMenus = Repository.DbContext.Set<RoleMenu>().Where(x => userRoles.Contains(x.RoleId)).Select(x => x.MenuId).ToArray();
            var menuList = _mapper.Map<IEnumerable<MenuList>>(Repository.DbContext.Set<Menu>().Where(x => x.MenuType != MenuConst.MENU_TYPE_BUTTON && userMenus.Contains(x.Id)));
            var rootList = menuList.Where(x => x.ParentId == 0);
            TreeUtil.BuildTree<MenuList>(menuList, rootList);
            return rootList;
        }

        public List<Role> FindRolesByUserId(long id)
        {
            var list = (from ur in Repository.DbContext.Set<UserRole>()
                        join r in Repository.DbContext.Set<Role>()
                        on ur.RoleId equals r.Id
                        where ur.UserId == id && r.Deleted == 0
                        select r).ToList();
            return list;
        }

        public List<Menu> FindMenusByUserId(long id)
        {
            var list = (from um in Repository.DbContext.Set<UserRole>()
                        join rm in Repository.DbContext.Set<RoleMenu>() on um.RoleId equals rm.RoleId
                        join m in Repository.DbContext.Set<Menu>() on rm.MenuId equals m.Id
                        where um.UserId == id && m.Deleted == 0
                        select m).ToList();
            return list;
        }

        public List<Menu> FindPermissionsByUserId(long id)
        {
            throw new NotImplementedException();
        }
    }
}
