using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MISA.Common.Model;
using MISA.QLTS.DataLayer;
using MISA.QLTS.DataLayer.DBConnection;
using MISA.QLTS.DataLayer.Entitys;
using MISA.QLTS.DataLayer.Interface;
using MISA.QLTS.Service;
using MISA.QLTS.Service.Interface;
using MISA.QLTS.Service.Service;
using MISA.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Cấu hình DI
            //Gọi các interface ở đây
            //base
            services.AddScoped(typeof(IBaseData<>), typeof(BaseDBConnection<>));
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IDbContext<>), typeof(DbconnectionV1<>));
            //DataLayer
            services.AddScoped<IDbConnectionAsset, DbConnectionAsset>();
            services.AddScoped<IDbConnectionAssetType, DbConnectionAssetType>();
            services.AddScoped<IDbConnectionDepartment, DbConnectionDepartment>();
            //Service
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetTypeService, AssetTypeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            // xu ly cors
            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MISA.QLTS.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MISA.QLTS.API v1"));
            }

            //exception handle (Xử lý exception chung )
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = exceptionHandlerPathFeature.Error;

                // Khởi tạo thông báo lỗi
                var errorMsg = new ErrorMsg();
                errorMsg.DevMsg = exception.Message;
                errorMsg.UserMsg.Add(MISA.QLTS.Common.Properties.Resources.Error_UserMsg);

                await context.Response.WriteAsJsonAsync(new { errorMsg });
            }));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
