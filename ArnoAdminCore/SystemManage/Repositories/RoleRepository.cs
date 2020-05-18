using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(SystemDbContext context) : base(context) { }
    }
}
