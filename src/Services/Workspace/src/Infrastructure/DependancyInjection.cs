using Application.Common.Interfaces;
using Application.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureDependancy(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddDbContext<ApplicationDbContext>
                (
                option => option.UseSqlServer
                    (
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            service.AddTransient<IDateTime, DateTimeService>();
            service.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            service.AddScoped<IProjectRepository, ProjectRepository>();
            service.AddScoped<IMethodesRepository, MethodesRepository>();
            service.AddScoped<IFolderRepository, FolderRepository>();
            service.AddScoped<IFileRepository, FileRepository>();

            return service;
        }

    }
}
