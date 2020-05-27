using ArnoAdminCore.Base.Services.Impl;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Services.Impl
{
    public class MenuService : BaseCRUDService<Menu>, IMenuService
    {
        public MenuService(MenuRepository repo) : base(repo)
        {

        }
    }
}
