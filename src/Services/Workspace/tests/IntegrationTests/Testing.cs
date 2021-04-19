using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using System;
using System.IO;
using System.Threading.Tasks;
using Web;

//Golobal test resources
namespace IntegrationTests
{
    //when you want to create a single test context and share it among tests 
    //in several test classes, and have it cleaned up after all the tests in the
    //test classes have finished.
    //[CollectionDefinition(nameof(DatabaseCollection))]
    //public class DatabaseCollection:ICollectionFixture<Testing> { }
    [SetUpFixture]
    public class Testing 
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _ckeckpoint;

        //Configuration of database"configuration string and //TODO configuration identity "
        [OneTimeSetUp]
        public void RunBeforeAnyTest()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .AddEnvironmentVariables();

            _configuration = builder.Build();

            //configure our services that we're going to use
            var services = new ServiceCollection();
            //we gonna use the same configuration in ConfigureServices in startup class
            var startup = new Startup(_configuration);
            //we need to mock the webService
            services.AddSingleton(Mock.Of<IWebHostEnvironment>(
                w => w.ApplicationName == "Web"
                && w.EnvironmentName == "Developement"));

            //Create services within a seperete scope to make tests separated
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            startup.ConfigureServices(services);

            ////check the database if already exist with Respawn
            //_ckeckpoint = new Checkpoint
            //{
            //    TablesToIgnore = new[] { "__EFMigrationHistory" }
            //};

        }

        ////to reset the database for the second test 
        ////Ps : we need a setup logic for any test we create so it's be shared
        //public static async Task ResetState()
        //{
        //    await _ckeckpoint.Reset(_configuration.GetConnectionString("DefaultConnection"));
        //}
        //with this meyhode we can pass any Entity or object
        public static async Task AddAsync<TEntity>(TEntity entity)
                where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Add(entity);
            await context.SaveChangesAsync();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetService<IMediator>();
            return await mediator.Send(request);
        }
    }
}
