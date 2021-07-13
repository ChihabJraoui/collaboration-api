using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Collaboration.ShareDocs.Persistence
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddPersistenceDependancy(this IServiceCollection services, IConfiguration configuration)
        {
            var signinKey = configuration["Jwt:SigningKey"];
            var conString = configuration.GetConnectionString("default");
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(conString, b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
                    {
                        config.SignIn.RequireConfirmedEmail      = false;
                        config.Password.RequireDigit             = false;
                        config.Password.RequireLowercase         = false;
                        config.Password.RequireUppercase         = false;
                        config.Password.RequireNonAlphanumeric   = false;
                        config.Password.RequiredLength           = 8;
                        config.User.RequireUniqueEmail           = true;
                    })
                    .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();
            services.AddAuthentication()
                    .AddIdentityServerJwt();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie(cfg => cfg.SlidingExpiration = true)
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };

                });
            services.AddAuthorization();
            services.AddTransient<IDateTime, DateTimeRepository>();
            services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IMethodesRepository, MethodesRepository>();
            services.AddScoped<IFolderRepository, FolderRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<INotificationApplicationUser, UserNotificationRepository>();
            services.AddTransient<IUserProjectRepository, UserProjectRepository>();
            //services.AddScoped<IRepositoryBase<Folder>, FolderRepository>();
            return services;
        }
    }
}
