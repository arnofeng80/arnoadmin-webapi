using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto;
using ArnoAdminCore.SystemManage.Models.Dto.List;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
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
        public Result list()
        {
            var list = _mapper.Map<IEnumerable<DepartmentList>>(_deptRepo.FindAll());

            return Result.Ok(list);
        }

        [HttpGet("all")]
        public Result All()
        {
            return Result.Ok(_mapper.Map<IEnumerable<DepartmentList>>(_deptRepo.FindAll()));
        }

        [HttpGet("{id}")]
        public Result GetDepartment(long id)
        {
            var dept = _deptRepo.FindById(id);

            if (dept == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(dept);
        }

        [HttpPost]
        public Result Add(Department entity)
        {
            _deptRepo.Add(entity);
            _deptRepo.Save();
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(Department entity)
        {
            _deptRepo.Update(entity);
            _deptRepo.Save();
            return Result.Ok();
        }

        [HttpDelete("{id}")]
        public Result Delete(long id)
        {
            _deptRepo.Delete(id);
            _deptRepo.Save();
            return Result.Ok();
        }

        [HttpGet("DeptTree")]
        public Result DeptTree()
        {
            var deptList = _mapper.Map<IEnumerable<DepartmentList>>(_deptRepo.FindAll());
            var rootList = deptList.Where(x => x.ParentId == 0);
            TreeUtil.BuildTree<DepartmentList>(deptList, rootList);
            return Result.Ok(rootList);
        }
    }
}