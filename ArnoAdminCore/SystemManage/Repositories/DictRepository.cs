using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.Base.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Repositories
{
    public class DictRepository : BaseRepository<Dict>
    {
        public DictRepository(SystemDbContext context) : base(context) { }
        public async Task<IEnumerable<Dict>> FindByCodeAsync(String dictCode)
        {
            return await _context.Set<Dict>().Where(x => x.Deleted == 0 && x.Status == DictConst.NORMAL_ENABLE && x.DictCode == dictCode.Trim()).OrderBy(x => x.DictSort).ToListAsync();
        }
    }
}
