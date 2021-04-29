using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Persistence
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPersistenceDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration.GetConnectionString("default");
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(conString, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IDateTime, DateTimeRepository>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IMethodesRepository, MethodesRepository>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IRepositoryBase<Folder>, FolderRepository>();

            return services;
        }
    }
}
