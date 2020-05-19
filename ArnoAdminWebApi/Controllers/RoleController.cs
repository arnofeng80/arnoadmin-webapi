using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.SystemManage.Services;
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
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly ILogger<DeptController> _logger;

        public RoleController(ILogger<DeptController> logger, IRoleService roleService, IMapper mapper)
        {
            _logger = logger;
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("list")]
        public Result PageList(RoleSearch search)
        {
            PageList<Role> list = _roleService.FindAll(search);
            return Result.Ok(list);
        }

        [HttpGet("all")]
        public Result All()
        {
            return Result.Ok(_roleService.FindAll());
        }

        [HttpGet("{id}")]
        public Result GetRole(long id)
        {
            var entity = _roleService.FindById(id);

            if (entity == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(entity);
        }

        [HttpPost]
        public Result Add(Role entity)
        {
            _roleService.AddWithMenu(entity);
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(Role entity)
        {
            foreach (RoleMenu rm in entity.RoleMenus)
            {
                rm.Role = entity;
            }
            _roleService.UpdateWithMenu(entity);
            return Result.Ok();
        }

        [HttpPut("updateDataScope")]
        public Result UpdateWithDept(Role entity)
        {
            foreach (RoleDept rd in entity.RoleDepts)
            {
                rd.Role = entity;
            }
            _roleService.UpdateWithDept(entity);
            return Result.Ok();
        }

        [HttpDelete]
        public Result Delete(long[] ids)
        {
            foreach(long id in ids)
            {
                _roleService.Delete(id);
            }
            return Result.Ok();
        }

        [HttpGet("menu/{roleId}")]
        public async Task<Result> FindMenuByRoleId(long roleId)
        {
            return Result.Ok(await _roleService.FindMenusByRoleIdAsync(roleId));
        }

        [HttpGet("dept/{roleId}")]
        public async Task<Result> FindDeptByRoleId(long roleId)
        {
            return Result.Ok(await _roleService.FindDeptByRoleIdAsync(roleId));
        }

        [HttpPut("changeStatus")]
        public Result ChangeStatus(Role entity)
        {
            Role role = _roleService.FindById(entity.Id);
            role.Status = entity.Status;
            _roleService.Update(role);
            _roleService.Save();

            return Result.Ok();
        }
    }
}