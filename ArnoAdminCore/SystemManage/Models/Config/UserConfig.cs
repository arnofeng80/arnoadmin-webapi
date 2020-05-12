using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Models.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("SysUser");
            //builder.HasOne(x => x.Department).WithMany(x => x.Users).HasForeignKey(x => x.DeptId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
