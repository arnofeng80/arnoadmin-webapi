using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Repositories
{
    public class MenuRepository : BaseRepository<Menu>
    {
        public MenuRepository(SystemDbContext context) : base(context)  { }

    }
}
