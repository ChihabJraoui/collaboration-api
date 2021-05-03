using Collaboration.ShareDocs.Api.Configurations;
using Collaboration.ShareDocs.Provision;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Collaboration.ShareDocs.Application;
using System.Reflection;
using Collaboration.ShareDocs.Api.Middlwares;
using Collaboration.ShareDocs.Application.Commands.Users;
using Collaboration.ShareDocs.Application.Common.Behaviours;
using Collaboration.ShareDocs.Persistence;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            services.AddPersistenceDependancy(Configuration);
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
            services.AddWebDependancy();
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
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();



            app.UseSwaggerSetup(this.Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
