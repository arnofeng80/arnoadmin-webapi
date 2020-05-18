using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Config
{
    public class RoleDeptConfig : IEntityTypeConfiguration<RoleDept>
    {
        public void Configure(EntityTypeBuilder<RoleDept> builder)
        {
            builder.ToTable("SysRoleDept");
            builder.HasKey(x => new { x.RoleId, x.DeptId });
            builder.HasOne(x => x.Role).WithMany(x => x.RoleDepts).HasForeignKey(x => x.RoleId);
            builder.HasOne(x => x.Department).WithMany(x => x.RoleDepts).HasForeignKey(x => x.DeptId);
        }
    }
}
