using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArnoAdminCore.SystemManage.Models.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("SysDept").HasKey(p => p.Id);
            builder.Property(p => p.DeptName).HasMaxLength(30).IsRequired(true);
        }
    }
}
