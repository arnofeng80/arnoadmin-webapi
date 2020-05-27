using ArnoAdminCore.Base.Services.Impl;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Repositories;

namespace ArnoAdminCore.SystemManage.Services.Impl
{
    public class DepartmentService : BaseCRUDService<Department>, IDepartmentService
    {
        public DepartmentService(DepartmentRepository repo) : base(repo) { }
    }
}
