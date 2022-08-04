using DIGEIG.Api.Security;
using DIGEIG.Application;
using DIGEIG.Application.Interfaces.Core;
using DIGEIG.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace DIGEIG.Api
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
            services.AddSpaStaticFiles(options => options.RootPath = "client-app/dist");
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(Configuration);
            services.AddScoped(typeof(ICurrentUserService), typeof(CurrentUserService));
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("SpaLocal", builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DIGEIG.Api", Version = "v1" });
            });
            services.AddHealthChecks();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DIGEIG.Api v1"));
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("SpaLocal");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";
                if (env.IsDevelopment())
                {
                    // Launch development server for Vue.js
                    spa.UseVueDevelopmentServer();
                }
            });
        }
    }
}
