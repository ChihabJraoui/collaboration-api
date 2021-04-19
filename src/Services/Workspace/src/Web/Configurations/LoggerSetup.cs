using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Configurations
{
    public static class LoggerSetup
    {
        public static Serilog.Core.Logger CreateLogger(IConfiguration configuration)
        {
            var logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(configuration)
                         .CreateLogger();
            return logger;
        }
    }
}
