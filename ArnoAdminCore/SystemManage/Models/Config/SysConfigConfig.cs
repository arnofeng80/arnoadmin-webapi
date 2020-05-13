using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArnoAdminCore.SystemManage.Models.Config
{
    public class SysConfigConfig : IEntityTypeConfiguration<SysConfig>
    {
        public void Configure(EntityTypeBuilder<SysConfig> builder)
        {
            builder.ToTable("SysConfig");
        }
    }
}
