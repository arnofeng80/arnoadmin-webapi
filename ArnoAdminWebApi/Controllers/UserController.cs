using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.SystemManage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            _userService.Update(user);

            return Result.Ok();
        }
    }
}
