using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;

namespace ArnoAdminCore.SystemManage.Repositories
{
    public class DictRepository : BaseRepository<Dict>
    {
        public DictRepository(SystemDbContext context) : base(context) { }
    }
}
