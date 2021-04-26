using Collaboration.ShareDocs.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistenceDependancy(configuration);

            return services;
        }
    }
}
