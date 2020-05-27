using ArnoAdminCore.Base.Models;
using ArnoAdminCore.SystemManage.Models.Poco;
using ArnoAdminCore.SystemManage.Models.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using ArnoAdminCore.Utils;

namespace ArnoAdminCore.Context
{
    public class SystemDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
                .UseLoggerFactory(MyLoggerFactory);

        //public override EntityEntry<TEntity> Add<TEntity>(TEntity entity)
        //{
        //    BaseEntity baseEntity = entity as BaseEntity;
        //    if (baseEntity != null)
        //    {
        //        baseEntity.Create();
        //    }
        //    return base.Add(entity);
        //}

        public override int SaveChanges()
        {
            ChangeTracker.Entries().Where(e => e.State == EntityState.Added && e.Entity is BaseEntity).ToList().ForEach(e => {
                var entry = (BaseEntity)e.Entity;
                entry.Id = IdGenerator.GetId();
                entry.CreateTime = entry.UpdateTime = DateTime.Now;
                entry.CreateBy = entry.UpdateBy = 0;
                entry.Deleted = 0;
            });

            ChangeTracker.Entries().Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity).ToList().ForEach(e => ((BaseEntity)e.Entity).UpdateTime = DateTime.Now);

            //ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted && e.Entity is BaseEntity).ToList().ForEach(e => {
            //    var entry = (BaseEntity)e.Entity;
            //    entry.UpdateTime = DateTime.Now;
            //    entry.UpdateBy = 0;
            //    entry.Deleted = 1;
            //    e.State = EntityState.Modified;
            //});
            return base.SaveChanges();
        }
    }
}
