using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Auth;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.List;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repository;
using ArnoAdminCore.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ArnoAdminWebApi.Controllers
{
    [Route("/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, UserRepository userRepo, IMapper mapper)
        {
            this._logger = logger;
            this._userRepo = userRepo;
            this._mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<Result> Login(LoginBody loginBody)
        {
            var user = await _userRepo.DbContext.Set<User>().Where(x => x.LoginName == loginBody.username).FirstOrDefaultAsync();
            if (user == null)
            {
                return Result.Error("用戶名或密碼錯誤");
            }

            return Result.Ok();
        }

        [HttpPost("logout")]
        public Result Logout()
        {
            return Result.Ok();
        }

        [HttpGet("getInfo")]
        public Result getInfo()
        {
            var user = _userRepo.DbContext.Set<User>().Where(x => x.LoginName == "admin").FirstOrDefault();
            var userRoles =_userRepo.DbContext.Set<UserRole>().Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToArray();
            var userMenus = _userRepo.DbContext.Set<RoleMenu>().Where(x => userRoles.Contains(x.RoleId)).Select(x => x.MenuId).ToArray();
            return Result.Ok(new { user = user, roles = userRoles, permissions = userMenus });
        }

        [HttpGet("getRouters")]
        public Result getRouters()
        {
            var user = _userRepo.DbContext.Set<User>().Where(x => x.LoginName == "admin").FirstOrDefault();
            var userRoles = _userRepo.DbContext.Set<UserRole>().Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToArray();
            var userMenus = _userRepo.DbContext.Set<RoleMenu>().Where(x => userRoles.Contains(x.RoleId)).Select(x => x.MenuId).ToArray();


            var menuList = _mapper.Map<IEnumerable<MenuList>>(_userRepo.DbContext.Set<Menu>().Where(x => userMenus.Contains(x.Id)));
            var rootList = menuList.Where(x => x.ParentId == 0);
            TreeUtil.BuildTree<MenuList>(menuList, rootList);
            return Result.Ok(rootList);
        }
    }
}