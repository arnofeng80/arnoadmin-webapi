using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
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
        private readonly DictRepository _dictRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<DictController> _logger;

        public DictController(ILogger<DictController> logger, DictRepository dictRepo, IMapper mapper)
        {
            _logger = logger;
            _dictRepo = dictRepo ?? throw new ArgumentNullException(nameof(dictRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("dictCode/{dictCode}")]
        public async Task<Result> dictCode(String dictCode)
        {
            var list = await _dictRepo.FindByCodeAsync(dictCode);
            return Result.Ok(list);
        }

        [HttpGet("list")]
        public Result list([FromQuery]DictSearch search)
        {
            PageList<Dict> list = _dictRepo.FindAll(search);
            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public Result GetDict(long id)
        {
            var dict = _dictRepo.FindById(id);

            if (dict == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(dict);
        }

        [HttpPost]
        public Result Add(Dict entity)
        {
            _dictRepo.Add(entity);
            _dictRepo.Save();
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(Dict entity)
        {
            _dictRepo.Update(entity);
            _dictRepo.Save();
            return Result.Ok();
        }

        [HttpDelete("{id}")]
        public Result Delete(long id)
        {
            _dictRepo.Delete(id);

            _dictRepo.Save();
            return Result.Ok();
        }
    }
}