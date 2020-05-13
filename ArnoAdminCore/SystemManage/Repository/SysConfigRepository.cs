using ArnoAdminCore.Base.Repository;
using ArnoAdminCore.SystemManage.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Repository
{
    public class SysConfigRepository : BaseRepository<SysConfig>
    {
        public SysConfigRepository(SystemDbContext context) : base(context) { }

        public async Task<IEnumerable<SysConfig>> FindByKeyAsync(String configKey)
        {
            return await _context.Set<SysConfig>().Where(x => x.Deleted == 0  && x.ConfigKey == configKey.Trim()).ToListAsync();
        }
    }
}
