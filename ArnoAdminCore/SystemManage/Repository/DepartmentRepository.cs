using ArnoAdminCore.Base.Repository;
using ArnoAdminCore.SystemManage.Context;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArnoAdminCore.SystemManage.Repository
{
    public class DepartmentRepository : BaseRepository<Department>
    {
        public DepartmentRepository(SystemDbContext context) : base(context) { }
    }
}
