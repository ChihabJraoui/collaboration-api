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
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using Collaboration.ShareDocs.Persistence.Hubs;

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
            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(origin => true);
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
            //upload file
            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            services.AddSwaggerSetup(this.Configuration);
            services.AddHttpContextAccessor();
            services.AddSignalR();

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

            // CORS
            app.UseCors("CorsPolicy");
            

            //file section
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseSwaggerSetup(this.Configuration);

            //SignalR
            //app.UseSignalR(route =>
            //{
            //    route.MapHub<NotificationHub>("SignalService");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/SignalService");
                endpoints.MapHub<MessageHub>("/messages");
                endpoints.MapControllers();
                
            });
        }
    }
}
