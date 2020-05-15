using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArnoAdminWebApi.Controllers
{
    [Route("sys/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleRepository _roleRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<DeptController> _logger;

        public RoleController(ILogger<DeptController> logger, RoleRepository roleRepo, IMapper mapper)
        {
            _logger = logger;
            _roleRepo = roleRepo ?? throw new ArgumentNullException(nameof(roleRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("list")]
        public async Task<Result> PageList([FromQuery]RoleSearch search)
        {
            PageList<Role> list = await _roleRepo.FindAllAsync(search);
            return Result.Ok(list);
        }

        [HttpGet("all")]
        public async Task<Result> All()
        {
            return Result.Ok(await _roleRepo.FindAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<Result> GetRole(long id)
        {
            var entity = await _roleRepo.FindByIdAsync(id);

            if (entity == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(entity);
        }

        [HttpPost]
        public async Task<Result> Add(Role entity)
        {
            _roleRepo.Add(entity);
            await _roleRepo.SaveAsync();
            return Result.Ok();
        }

        [HttpPut]
        public async Task<Result> Update(Role entity)
        {
            using(var tran = _roleRepo.DbContext.Database.BeginTransaction())
            {
                foreach (RoleMenu rm in entity.RoleMenus)
                {
                    rm.Role = entity;
                }
                _roleRepo.Update(entity);
                await _roleRepo.SaveAsync();
                tran.Commit();
            }
            
            return Result.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<Result> Delete(long id)
        {
            _roleRepo.Delete(id);
            await _roleRepo.SaveAsync();
            return Result.Ok();
        }

        [HttpGet("menu/{roleId}")]
        public async Task<Result> FindMenuByRoleId(long roleId)
        {
            return Result.Ok(await _roleRepo.FindMenusByRoleIdAsync(roleId));;
        }
    }
}