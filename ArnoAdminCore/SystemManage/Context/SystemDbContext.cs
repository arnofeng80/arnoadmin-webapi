using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Models.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArnoAdminCore.SystemManage.Context
{
    public class SystemDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Dict> Dicts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepartmentConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            modelBuilder.ApplyConfiguration(new DictConfig());
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>().HasOne(x => x.Department).WithMany(x => x.Users)
            //    .HasForeignKey(x => x.DeptId).OnDelete(DeleteBehavior.Restrict);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLoggerFactory(MyLoggerFactory);

        public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        {
            BaseEntity baseEntity = entity as BaseEntity;
            if(baseEntity != null)
            {
                baseEntity.Create();
            }
            return base.Add(entity);
        }
    }
}
