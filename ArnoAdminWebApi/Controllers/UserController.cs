﻿using ArnoAdminCore.Auth;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Services;
using ArnoAdminCore.Utils;
using ArnoAdminCore.Utils.Excel;
using ArnoAdminCore.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ArnoAdminWebApi.Controllers
{
    [Route("/sys/user")]
    [ApiController]
    [AuthFilter]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        [HttpPost("list")]
        public Result PageList(UserSearch search)
        {
            PageList<User> list = _userService.FindPage(search);
            return Result.Ok(list);
        }

        [HttpGet("all")]
        public Result All()
        {
            return Result.Ok(_userService.FindAll());
        }

        [HttpGet("{id}")]
        public Result FindById(long id)
        {
            var user = _userService.FindById(id);
            if (user == null)
            {
                return Result.Error("Not Found");
            }
            return Result.Ok(user);
        }

        [HttpPost]
        public Result Add(User entity)
        {
            entity.Password = EncryptHelper.PasswordEncoding(entity.Password.Trim());
            _userService.Add(entity);
            return Result.Ok();
        }

        [HttpPut]
        [AuthFilter("sys:user:edit")]
        public Result Update(User entity)
        {
            foreach (UserRole ur in entity.UserRoles)
            {
                ur.User = entity;
            }
            if(!String.IsNullOrWhiteSpace(entity.Password))
            {
                entity.Password = EncryptHelper.PasswordEncoding(entity.Password.Trim());
            }
            _userService.UpdateWithRole(entity);
            return Result.Ok();
        }

        [HttpDelete]
        public Result Delete(long[] ids)
        {
            foreach (long id in ids)
            {
                _userService.Delete(id);
            }
            return Result.Ok();
        }

        [HttpPut("changeStatus")]
        public Result ChangeStatus(User entity)
        {
            User user = _userService.FindById(entity.Id);
            if (user == null)
            {
                return Result.Error("User Not Found");
            }
            user.Status = entity.Status;
            _userService.UpdatePartial(user);

            return Result.Ok();
        }

        [HttpPut("resetPwd")]
        public Result ResetPwd(User entity)
        {
            User user = _userService.FindById(entity.Id);
            if (user == null)
            {
                return Result.Error("User Not Found");
            }
            user.Password = EncryptHelper.PasswordEncoding(entity.Password.Trim());
            _userService.UpdatePartial(user);

            return Result.Ok();
        }

        [HttpGet("roles/{userId}")]
        public Result FindRoleByUserId(long userId)
        {
            return Result.Ok(_userService.FindRolesByUserId(userId));
        }

        [HttpGet("findMenuTree")]
        public Result FindRouters()
        {
            return Result.Ok(_userService.FindMenuTreeByUserId(Current.Operator.Id));
        }

        [HttpGet("getRolesAndPermissions")]
        public Result getInfo()
        {
            return Result.Ok(Current.Operator);
        }

        [HttpPost("importData")]
        public Result importData(IFormCollection fileList)
        {
            List<User> list = null;
            if (fileList.Files.Count > 0)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fileList.Files[0].CopyTo(ms);
                    ms.Flush();
                    ms.Position = 0;
                    ExcelHelper<User> excel = new ExcelHelper<User>();
                    list = excel.ImportFromExcel(ms);
                }
            }

            if(list != null && list.Count > 0)
            {
                foreach(User u in list)
                {
                    u.Password = EncryptHelper.PasswordEncoding(u.Password.Trim());
                }
            }
            return Result.Ok();
        }

        [HttpPost("export")]
        public FileResult exportData(UserSearch search)
        {
            var list = _userService.FindAll(search);
            ExcelHelper<User> excel = new ExcelHelper<User>();
            var actionresult = new FileStreamResult(excel.CreateExportMemoryStream(list.ToList(), "", null), "application/vnd.ms-excel");
            return actionresult;
        }

    }
}
