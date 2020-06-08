using System;
using System.Linq;
using ArnoAdminCore.Auth;
using ArnoAdminCore.Base.Models;
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
            _userService.Logout();
            return Result.Ok();
        }
    }
}