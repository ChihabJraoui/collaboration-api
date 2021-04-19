using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Configurations
{
    using Application.Common.Interfaces;
    using Web.Filters;

    public static class DependancyInjection
    {
        public static IServiceCollection AddWebDependancy( this IServiceCollection services) 
        {
            services.AddScoped<ApiExceptionFilter>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
