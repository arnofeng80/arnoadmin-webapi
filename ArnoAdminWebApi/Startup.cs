using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArnoAdminCore.Auth;
using ArnoAdminCore.Base;
using ArnoAdminCore.Base.Repositories;
using ArnoAdminCore.Cache;
using ArnoAdminCore.Context;
using ArnoAdminCore.SystemManage.Repositories;
using ArnoAdminCore.SystemManage.Services;
using ArnoAdminCore.SystemManage.Services.Impl;
using ArnoAdminCore.Utils;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
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
            #region Session
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            // 啟用分布式緩存(該步驟需在AddSession()調用前使用)
            //services.AddDistributedMemoryCache();//啟用session之前必須先添加分布式緩存

            //services.AddSession(options =>
            //{
            //    options.Cookie.Name = ".AdventureWorks.Session";
            //    options.IdleTimeout = TimeSpan.FromSeconds(1800);//設置session的過期時間
            //    options.Cookie.HttpOnly = true;//設置在瀏覽器不能通過js獲得該cookie的值
            //});


            services.AddDistributedMemoryCache();
            services.AddMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            #endregion

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Converters.Add(new LongJsonConverter());
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss"; //設置時間格式
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; //忽略迴圈引用
                //options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); //數據格式首字母小寫
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver(); //數據格式按原樣輸出
                //options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore; //忽略空值
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
            GlobalContext.Services = services;

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

            GlobalContext.ServiceProvider = app.ApplicationServices;
        }
    }
}
