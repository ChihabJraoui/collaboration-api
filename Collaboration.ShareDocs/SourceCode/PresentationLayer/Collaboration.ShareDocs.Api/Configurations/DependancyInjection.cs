using Collaboration.ShareDocs.Api.Services;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Configurations
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddWebDependancy(this IServiceCollection services)
        {
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
