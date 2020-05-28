using System;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.SystemManage.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArnoAdminWebApi.Controllers
{
    [Route("sys/config")]
    [ApiController]
    public class SysConfigController : ControllerBase
    {
        private readonly IConfigService _configService;
        private readonly IMapper _mapper;
        private readonly ILogger<SysConfigController> _logger;

        public SysConfigController(ILogger<SysConfigController> logger, IConfigService configService, IMapper mapper)
        {
            _logger = logger;
            _configService = configService ?? throw new ArgumentNullException(nameof(configService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("configKey/{configKey}")]
        public Result configKey(String configKey)
        {
            return Result.Ok(_configService.FindByKey(configKey));
        }

        [HttpPost("list")]
        public Result list(SysConfigSearch search)
        {
            PageList<SysConfig> list = _configService.FindPage(search);
            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public Result GetSysConfig(long id)
        {
            var sysConfig = _configService.FindById(id);

            if (sysConfig == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(sysConfig);
        }

        [HttpPost]
        public Result Add(SysConfig entity)
        {
            _configService.Add(entity);
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(SysConfig entity)
        {
            _configService.Update(entity);
            return Result.Ok();
        }

        [HttpDelete]
        public Result Delete(long[] ids)
        {
            foreach (long id in ids)
            {
                _configService.Delete(id);
            }
            return Result.Ok();
        }
    }
}