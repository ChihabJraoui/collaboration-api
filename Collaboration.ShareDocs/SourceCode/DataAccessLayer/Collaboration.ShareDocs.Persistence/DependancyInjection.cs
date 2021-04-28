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

            return services;
        }
    }
}
