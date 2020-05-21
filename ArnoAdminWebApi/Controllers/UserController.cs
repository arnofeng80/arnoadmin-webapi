using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.SystemManage.Services;
using ArnoAdminCore.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ArnoAdminWebApi.Controllers
{
    [Route("/sys/user")]
    [ApiController]
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
            PageList<User> list = _userService.FindAll(search);
            return Result.Ok(list);
        }

        [HttpGet("all")]
        public Result All()
        {
            return Result.Ok(_userService.FindAll());
        }

        [HttpGet("{id}")]
        public Result GetUser(long id)
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
            _userService.Add(entity);
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(User entity)
        {
            foreach (UserRole ur in entity.UserRoles)
            {
                ur.User = entity;
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
            user.Password = entity.Password;
            _userService.UpdatePartial(user);

            return Result.Ok();
        }

        [HttpGet("dept/{userId}")]
        public async Task<Result> FindRoleByUserId(long userId)
        {
            return Result.Ok(await _userService.FindRoleByUserIdAsync(userId));
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
            return Result.Ok();
        }

        public Result exportData()
        {
            return Result.Ok();
        }
    }
}
