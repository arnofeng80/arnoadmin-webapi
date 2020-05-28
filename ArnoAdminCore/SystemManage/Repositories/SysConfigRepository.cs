using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Repositories
{
    public class SysConfigRepository : BaseRepository<SysConfig>
    {
        public SysConfigRepository(SystemDbContext context) : base(context) { }
    }
}
