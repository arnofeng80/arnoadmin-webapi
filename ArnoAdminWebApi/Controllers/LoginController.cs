using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Auth;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.List;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Services;
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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IUserService userService, IMapper mapper)
        {
            this._logger = logger;
            this._userService = userService;
            this._mapper = mapper;
        }

        [HttpPost("login")]
        public Result Login(LoginBody loginBody)
        {
            if(String.IsNullOrWhiteSpace(loginBody.UserName) || String.IsNullOrWhiteSpace(loginBody.Password))
            {
                return Result.Error("請輸入用戶名和密碼");
            }

            User user = null;
            try
            {
                user = _userService.Login(loginBody.UserName.Trim(), EncryptHelper.HMACMD5Encoding(loginBody.Password.Trim()));
            }
            catch (Exception e)
            {
                return Result.Error(e.Message);
            }

            return Result.Ok(new { Token = user.Token });
        }

        [HttpPost("logout")]
        public Result Logout()
        {
            String aa = HttpContext.Session.GetString("code");
            return Result.Ok();
        }

        [HttpGet("getInfo")]
        public Result getInfo()
        {
            var user = _userService.Repository.DbContext.Set<User>().Where(x => x.LoginName == "admin").FirstOrDefault();
            var userRoles = _userService.Repository.DbContext.Set<UserRole>().Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToArray();
            var userMenus = _userService.Repository.DbContext.Set<RoleMenu>().Where(x => userRoles.Contains(x.RoleId)).Select(x => x.MenuId).ToArray();
            return Result.Ok(new { user = user, roles = userRoles, permissions = userMenus });
        }

        [HttpGet("getRouters")]
        public Result getRouters()
        {
            var user = _userService.Repository.DbContext.Set<User>().Where(x => x.LoginName == "admin").FirstOrDefault();
            var userRoles = _userService.Repository.DbContext.Set<UserRole>().Where(x => x.UserId == user.Id).Select(x => x.RoleId).ToArray();
            var userMenus = _userService.Repository.DbContext.Set<RoleMenu>().Where(x => userRoles.Contains(x.RoleId)).Select(x => x.MenuId).ToArray();
            var menuList = _mapper.Map<IEnumerable<MenuList>>(_userService.Repository.DbContext.Set<Menu>().Where(x => userMenus.Contains(x.Id)));
            var rootList = menuList.Where(x => x.ParentId == 0);
            TreeUtil.BuildTree<MenuList>(menuList, rootList);
            return Result.Ok(rootList);
        }
    }
}