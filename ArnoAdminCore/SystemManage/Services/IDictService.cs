using ArnoAdminCore.Base.Services;
using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;

namespace ArnoAdminCore.SystemManage.Services
{
    public interface IDictService : IBaseCRUDService<Dict>
    {
        public IEnumerable<Dict> FindByCode(String dictCode);
    }
}
