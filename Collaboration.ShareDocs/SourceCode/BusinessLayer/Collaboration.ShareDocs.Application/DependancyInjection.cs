using Collaboration.ShareDocs.Persistence;

using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using Collaboration.ShareDocs.Application.Common.Behaviours;


namespace Collaboration.ShareDocs.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistenceDependancy(configuration);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());               

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            return services;
        }
    }
}
