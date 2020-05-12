using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto;
using ArnoAdminCore.SystemManage.Models.Dto.List;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repository.Impl;
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
        private readonly DepartmentRepository _deptRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<DeptController> _logger;

        public DeptController(ILogger<DeptController> logger, DepartmentRepository deptRepo, IMapper mapper)
        {
            _logger = logger;
            _deptRepo = deptRepo ?? throw new ArgumentNullException(nameof(deptRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("list")]
        public async Task<Result> list()
        {
            var list = _mapper.Map<IEnumerable<DepartmentList>>(await _deptRepo.FindAllAsync());

            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetDepartment(long id)
        {
            var dept = await _deptRepo.FindByIdAsync(id);

            if (dept == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(dept);
        }

        [HttpPost]
        public async Task<Result> Add(Department entity)
        {
            _deptRepo.Add(entity);
            await _deptRepo.SaveAsync();
            return Result.Ok();
        }

        [HttpPut]
        public async Task<Result> Update(Department entity)
        {
            _deptRepo.Update(entity);
            await _deptRepo.SaveAsync();
            return Result.Ok();
        }

        [HttpDelete("{id}")]
        public async Task<Result> Delete(long id)
        {
            _deptRepo.Delete(id);
            await _deptRepo.SaveAsync();
            return Result.Ok();
        }

        [HttpGet("DeptTree")]
        public async Task<Result> DeptTree()
        {
            var deptList = _mapper.Map<IEnumerable<DepartmentList>>(await _deptRepo.FindAllAsync());
            var rootList = deptList.Where(x => x.ParentId == 0);
            TreeUtil.BuildTree<DepartmentList>(deptList, rootList);
            return Result.Ok(rootList);
        }
    }
}