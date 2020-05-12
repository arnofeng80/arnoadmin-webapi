using ArnoAdminCore.SystemManage.Models.Poco;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArnoAdminCore.SystemManage.Models.Config
{
    public class DictConfig : IEntityTypeConfiguration<Dict>
    {
        public void Configure(EntityTypeBuilder<Dict> builder)
        {
            builder.ToTable("SysDict");
        }
    }
}
