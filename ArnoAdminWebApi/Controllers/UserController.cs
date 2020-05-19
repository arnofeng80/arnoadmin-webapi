using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArnoAdminWebApi.Controllers
{
    [Route("/sys/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepo;
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger, UserRepository userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }
        [HttpGet("list")]
        public Result list([FromQuery]UserSearch search)
        {
            PageList<User> list = _userRepo.FindAll(search);
            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public Result GetUser(long id)
        {
            var user = _userRepo.FindById(id);
            if (user == null)
            {
                return Result.Error("Not Found");
            }
            return Result.Ok(user);
        }

        [HttpPost]
        public Result Add(User entity)
        {
            _userRepo.Add(entity);
            _userRepo.Save();
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(User entity)
        {
            _userRepo.Update(entity);
            _userRepo.Save();
            return Result.Ok();
        }

        [HttpDelete]
        public Result Delete(User entity)
        {
            _userRepo.Add(entity);
            return Result.Ok();
        }
    }
}
