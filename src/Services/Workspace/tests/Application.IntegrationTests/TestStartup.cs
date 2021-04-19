using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web;
using Xunit;

namespace Application.IntegrationTests
{
    public class TestStartup:Startup
    {
        private readonly IDateTime _dateTime;
        private readonly ICurrentUserService _currentUserService;
        public TestStartup(IConfiguration configuration,IDateTime dateTime,ICurrentUserService currentUserService) : base(configuration)
        {
            this._dateTime = dateTime;
            this._currentUserService=currentUserService;
        }

        public async Task Test1()
        {
          var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "testDb")
                .Options;
         using(var context = new ApplicationDbContext(options,_currentUserService,_dateTime))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
            }
           
        }
       
    }
}
