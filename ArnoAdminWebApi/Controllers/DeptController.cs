using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto;
using ArnoAdminCore.SystemManage.Models.Dto.List;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.SystemManage.Services;
using ArnoAdminCore.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArnoAdminWebApi.Controllers
{
    [Route("/sys/dept")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        private readonly IDepartmentService _deptService;
        private readonly IMapper _mapper;
        private readonly ILogger<DeptController> _logger;

        public DeptController(ILogger<DeptController> logger, IDepartmentService deptService, IMapper mapper)
        {
            _logger = logger;
            _deptService = deptService ?? throw new ArgumentNullException(nameof(deptService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("list")]
        public Result list(DeptSearch search)
        {
            return Result.Ok(_mapper.Map<PageList<DepartmentList>>(_deptService.FindPage(search)));
        }

        [HttpPost("all")]
        public Result All(DeptSearch search)
        {
            return Result.Ok(_mapper.Map<IEnumerable<DepartmentList>>(_deptService.FindAll(search)));
        }

        [HttpGet("{id}")]
        public Result GetDepartment(long id)
        {
            var dept = _deptService.FindById(id);

            if (dept == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(dept);
        }

        [HttpPost]
        public Result Add(Department entity)
        {
            return Result.Ok(_deptService.Add(entity));
        }

        [HttpPut]
        public Result Update(Department entity)
        {
            _deptService.Update(entity);
            return Result.Ok();
        }

        [HttpDelete("{id}")]
        public Result Delete(long id)
        {
            _deptService.Delete(id);
            return Result.Ok();
        }

        [HttpGet("DeptTree")]
        public Result DeptTree()
        {
            var deptList = _mapper.Map<IEnumerable<DepartmentList>>(_deptService.FindAll());
            var rootList = deptList.Where(x => x.ParentId == 0);
            TreeUtil.BuildTree<DepartmentList>(deptList, rootList);
            return Result.Ok(rootList);
        }
    }
}