using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Base;
using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.SystemManage.Services;
using ArnoAdminCore.SystemManage.Services.Impl;
using ArnoAdminCore.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ArnoAdminWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            
            GlobalContext.HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Session内存缓存
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            ////启用内存缓存(该步骤需在AddSession()调用前使用)
            //services.AddDistributedMemoryCache();//启用session之前必须先添加内存

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".AdventureWorks.Session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(1800);//设置session的过期时间
            //    options.Cookie.HttpOnly = true;//设置在浏览器不能通过js获得该cookie的值
            //});

            services.AddDistributedMemoryCache();
            services.AddSession();
            #endregion

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new LongJsonConverter());
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<DepartmentRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<DictRepository>();
            services.AddScoped<SysConfigRepository>();
            services.AddScoped<MenuRepository>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IDictService, DictService>();
            services.AddScoped<IConfigService, ConfigService>();

            GlobalContext.SystemConfig = Configuration.GetSection("SystemConfig").Get<SystemConfig>();

            services.AddDbContext<SystemDbContext>(options =>
                   options.UseSqlServer(GlobalContext.SystemConfig.DBConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
