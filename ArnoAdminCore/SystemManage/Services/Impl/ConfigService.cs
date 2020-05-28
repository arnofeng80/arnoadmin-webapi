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
    public class ConfigService : BaseCRUDService<SysConfig>, IConfigService
    {
        public ConfigService(SysConfigRepository repo) : base(repo)
        {

        }

        public IEnumerable<SysConfig> FindByKey(String configKey)
        {
            return Repository.DbContext.Set<SysConfig>().Where(x => x.Deleted == 0 && x.ConfigKey == configKey.Trim()).ToList();
        }
    }
}
