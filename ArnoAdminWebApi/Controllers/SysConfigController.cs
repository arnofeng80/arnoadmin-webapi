using System;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArnoAdminWebApi.Controllers
{
    [Route("sys/config")]
    [ApiController]
    public class SysConfigController : ControllerBase
    {
        private readonly SysConfigRepository _sysConfigRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<SysConfigController> _logger;

        public SysConfigController(ILogger<SysConfigController> logger, SysConfigRepository sysConfigRepo, IMapper mapper)
        {
            _logger = logger;
            _sysConfigRepo = sysConfigRepo ?? throw new ArgumentNullException(nameof(sysConfigRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("configKey/{configKey}")]
        public async Task<Result> configKey(String configKey)
        {
            var list = await _sysConfigRepo.FindByKeyAsync(configKey);
            return Result.Ok(list);
        }

        [HttpGet("list")]
        public Result list([FromQuery]SysConfigSearch search)
        {
            PageList<SysConfig> list = _sysConfigRepo.FindAll(search);
            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public Result GetSysConfig(long id)
        {
            var sysConfig = _sysConfigRepo.FindById(id);

            if (sysConfig == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(sysConfig);
        }

        [HttpPost]
        public Result Add(SysConfig entity)
        {
            _sysConfigRepo.Add(entity);
            _sysConfigRepo.Save();
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(SysConfig entity)
        {
            _sysConfigRepo.Update(entity);
            _sysConfigRepo.Save();
            return Result.Ok();
        }

        [HttpDelete("{id}")]
        public Result Delete(long id)
        {
            _sysConfigRepo.Delete(id);
            _sysConfigRepo.Save();
            return Result.Ok();
        }
    }
}