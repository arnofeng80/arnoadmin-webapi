using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Base.Models;
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
    [Route("sys/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly MenuRepository _menuRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuController> _logger;

        public MenuController(ILogger<MenuController> logger, MenuRepository menuRepo, IMapper mapper)
        {
            _logger = logger;
            _menuRepo = menuRepo ?? throw new ArgumentNullException(nameof(menuRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("list")]
        public Result list()
        {
            var list = _mapper.Map<IEnumerable<MenuList>>(_menuRepo.FindAll());
            return Result.Ok(list);
        }

        [HttpGet("{id}")]
        public Result GetMenu(long id)
        {
            var menu = _menuRepo.FindById(id);

            if (menu == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(menu);
        }

        [HttpPost]
        public Result Add(Menu entity)
        {
            _menuRepo.Add(entity);
            _menuRepo.Save();
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(Menu entity)
        {
            _menuRepo.Update(entity);
            _menuRepo.Save();
            return Result.Ok();
        }

        [HttpDelete("{id}")]
        public Result Delete(long id)
        {
            _menuRepo.Delete(id);
            _menuRepo.Save();
            return Result.Ok();
        }

        [HttpGet("TreeData")]
        public Result TreeData()
        {
            var menuList = _mapper.Map<IEnumerable<MenuList>>(_menuRepo.FindAll());
            var rootList = menuList.Where(x => x.ParentId == 0);
            TreeUtil.BuildTree<MenuList>(menuList, rootList);
            return Result.Ok(rootList);
        }
    }
}