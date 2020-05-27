using ArnoAdminCore.Base.Constants;
using ArnoAdminCore.Base.Services.Impl;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArnoAdminCore.SystemManage.Services.Impl
{
    public class DictService : BaseCRUDService<Dict>, IDictService
    {
        public DictService(DictRepository repo) : base(repo)
        {

        }
        public IEnumerable<Dict> FindByCode(String dictCode)
        {
            return Repository.DbContext.Set<Dict>().Where(x => x.Deleted == 0 && x.Status == DictConst.NORMAL_ENABLE && x.DictCode == dictCode.Trim()).OrderBy(x => x.DictSort).ToList();
        }
    }
}
