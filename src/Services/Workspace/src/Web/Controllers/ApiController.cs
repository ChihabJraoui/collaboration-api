using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    using Web.Filters;

    [
        Produces("application/json"),
        Route("api/[controller]"),
        ApiController,
        ServiceFilter(typeof(ApiExceptionFilter))
    ]
    public class ApiController:ControllerBase
    {
        public ApiController()
        {

        }
    }
}
