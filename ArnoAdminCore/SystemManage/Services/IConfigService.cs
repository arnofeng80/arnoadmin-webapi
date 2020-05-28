using ArnoAdminCore.Base.Services;
using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Services
{
    public interface IConfigService : IBaseCRUDService<SysConfig>
    {
        public IEnumerable<SysConfig> FindByKey(String configKey);
    }
}
