using Collaboration.ShareDocs.Api.Configurations;
using Collaboration.ShareDocs.Provision;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application;
using Collaboration.ShareDocs.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Collaboration.ShareDocs.Api.Middlwares;
using Collaboration.ShareDocs.Application.Commands.Users;

using FluentValidation.AspNetCore;
using Collaboration.ShareDocs.Api.Services;

namespace Collaboration.ShareDocs.Api
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
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5000",
                                       "https://localhost:4200"
                                       )
                                       .AllowAnyHeader()
                                       .AllowAnyMethod();
                    });
            });
            services.AddWebDependancy();
            services.AddApplicationDependancy(Configuration);
            services
                .AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetUserCommand>());
            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddControllers();
         
            services.AddSwaggerSetup(this.Configuration);
            services.AddHttpContextAccessor();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");



            app.UseSwaggerSetup(this.Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
