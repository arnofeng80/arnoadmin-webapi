using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        public async Task<Result> list([FromQuery]UserSearch search)
        {
            PageList<User> list = await _userRepo.FindAllAsync(search);
            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetUser(long id)
        {
            var user = await _userRepo.FindByIdAsync(id);
            if (user == null)
            {
                return Result.Error("Not Found");
            }
            return Result.Ok(user);
        }

        [HttpPost]
        public async Task<Result> Add(User entity)
        {
            _userRepo.Add(entity);
            await _userRepo.SaveAsync();
            return Result.Ok();
        }

        [HttpPut]
        public async Task<Result> Update(User entity)
        {
            _userRepo.Update(entity);
            await _userRepo.SaveAsync();
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
