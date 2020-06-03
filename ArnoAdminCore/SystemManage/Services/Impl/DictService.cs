using ArnoAdminCore.Base.Constants;
using ArnoAdminCore.Base.Services.Impl;
using ArnoAdminCore.Cache;
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

        [CacheEvict("dict")]
        public override Dict Add(Dict entity)
        {
            return base.Add(entity);
        }
        [CacheEvict("dict")]
        public override Dict Update(Dict entity)
        {
            return base.Update(entity);
        }
        [CacheEvict("dict")]
        public override Dict UpdatePartial(Dict entity)
        {
            return base.UpdatePartial(entity);
        }
        [CacheEvict("dict")]
        public override void Delete(Dict entity)
        {
            base.Delete(entity);
        }
        [CacheEvict("dict")]
        public override void Delete(long id)
        {
            base.Delete(id);
        }
        [Cacheable("dict")]
        public virtual IEnumerable<Dict> FindByCode(String dictCode)
        {
            return Repository.DbContext.Set<Dict>().Where(x => x.Deleted == 0 && x.Status == DictConst.NORMAL_ENABLE && x.DictCode == dictCode.Trim()).OrderBy(x => x.DictSort).ToList();
        }
    }
}
