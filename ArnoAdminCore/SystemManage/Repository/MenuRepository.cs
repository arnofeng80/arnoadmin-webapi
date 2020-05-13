using ArnoAdminCore.Base.Repository;
using ArnoAdminCore.SystemManage.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Repository
{
    public class MenuRepository : BaseRepository<Menu>
    {
        public MenuRepository(SystemDbContext context) : base(context)  { }

    }
}
