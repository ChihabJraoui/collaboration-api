using Collaboration.ShareDocs.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Provision
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddProvisionDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationDependancy(configuration);

            return services;
        }
    }
}
