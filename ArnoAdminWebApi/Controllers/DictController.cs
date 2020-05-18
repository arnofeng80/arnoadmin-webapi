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
        public async Task<Result> list([FromQuery]DictSearch search)
        {
            PageList<Dict> list = await _dictRepo.FindAllAsync(search);
            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<Result> GetDict(long id)
        {
            var dict = await _dictRepo.FindByIdAsync(id);

            if (dict == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(dict);
        }

        [HttpPost]
        public async Task<Result> Add(Dict entity)
        {
            _dictRepo.Add(entity);
            await _dictRepo.SaveAsync();
            return Result.Ok();
        }

        [HttpPut]
        public async Task<Result> Update(Dict entity)
        {
            _dictRepo.Update(entity);
            await _dictRepo.SaveAsync();
            return Result.Ok();
        }

        //[HttpDelete("{id}")]
        //public async Task<Result> Delete(long id)
        //{
        //    _dictRepo.Delete(id);
        //    await _dictRepo.SaveAsync();
        //    return Result.Ok();
        //}

        [HttpDelete("{id}")]
        public async Task<Result> Delete(long id)
        {
            _dictRepo.Delete(id);

            await _dictRepo.SaveAsync();
            return Result.Ok();
        }
    }
}