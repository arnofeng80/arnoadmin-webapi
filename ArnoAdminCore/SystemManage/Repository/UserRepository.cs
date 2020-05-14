using ArnoAdminCore.Base.Repository;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SystemDbContext context) : base(context) { }
    }
}
