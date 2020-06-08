using System;
using System.Collections.Generic;
using System.Linq;
using ArnoAdminCore.Auth;
using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Dto.List;
using ArnoAdminCore.SystemManage.Models.Dto.Search;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Services;
using ArnoAdminCore.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ArnoAdminWebApi.Controllers
{
    [Route("sys/menu")]
    [ApiController]
    [AuthFilter]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuController> _logger;

        public MenuController(ILogger<MenuController> logger, IMenuService menuService, IMapper mapper)
        {
            _logger = logger;
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost("all")]
        public Result All(MenuSearch search)
        {
            return Result.Ok(_mapper.Map<IEnumerable<MenuList>>(_menuService.FindAll(search)));
        }

        [HttpGet("{id}")]
        public Result FindById(long id)
        {
            var menu = _menuService.FindById(id);

            if (menu == null)
            {
                return Result.Error("Not Found");
            }

            return Result.Ok(menu);
        }

        [HttpPost]
        public Result Add(Menu entity)
        {
            _menuService.Add(entity);
            return Result.Ok();
        }

        [HttpPut]
        public Result Update(Menu entity)
        {
            _menuService.Update(entity);
            return Result.Ok();
        }

        [HttpDelete("{id}")]
        public Result Delete(long id)
        {
            _menuService.Delete(id);
            return Result.Ok();
        }

        [HttpGet("TreeData")]
        public Result TreeData()
        {
            var menuList = _mapper.Map<IEnumerable<MenuList>>(_menuService.FindAll());
            var rootList = menuList.Where(x => x.ParentId == 0);
            TreeUtil.BuildTree<MenuList>(menuList, rootList);
            return Result.Ok(rootList);
        }
    }
}