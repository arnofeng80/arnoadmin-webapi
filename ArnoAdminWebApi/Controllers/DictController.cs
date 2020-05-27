using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.SystemManage.Services;
using ArnoAdminCore.SystemManage.Services.Impl;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;

namespace ArnoAdminWebApi.Controllers
{
    [Route("sys/dict")]
    [ApiController]
    public class DictController : ControllerBase
    {
        private readonly IDictService _dictService;
        private readonly IMapper _mapper;
        private readonly ILogger<DictController> _logger;

        public DictController(ILogger<DictController> logger, IDictService dictService, IMapper mapper)
        {
            _logger = logger;
            _dictService = dictService ?? throw new ArgumentNullException(nameof(dictService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("dictCode/{dictCode}")]
        public Result dictCode(String dictCode)
        {
            var list = _dictService.FindByCode(dictCode);
            return Result.Ok(list);
        }

        [HttpPost("list")]
        public Result list(DictSearch search)
        {
            PageList<Dict> list = _dictService.FindPage(search);
            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public Result GetDict(long id)
        {
            var dict = _dictService.FindById(id);

            if (dict == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(dict);
        }

        [HttpPost]
        public Result Add(Dict entity)
        {
            _dictService.Add(entity);
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(Dict entity)
        {
            _dictService.Update(entity);
            return Result.Ok();
        }

        [HttpDelete]
        public Result Delete(long[] ids)
        {
            foreach (long id in ids)
            {
                _dictService.Delete(id);
            }
            return Result.Ok();
        }
    }
}