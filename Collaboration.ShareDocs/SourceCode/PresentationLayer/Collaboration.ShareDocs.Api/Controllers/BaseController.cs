using System.Net;
using Collaboration.ShareDocs.Application.Common.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Collaboration.ShareDocs.Api.Controllers
{

    [ApiController, Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => this._mediator ??= this.HttpContext.RequestServices.GetService<IMediator>();

        protected IActionResult FormatResponseToActionResult(ApiResponseDetails response)
        {
            switch ((HttpStatusCode)response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Ok(response.Object ?? response);
                case HttpStatusCode.BadRequest:
                    return BadRequest(response);
                case HttpStatusCode.NotFound:
                    return NotFound(response);
                case HttpStatusCode.Conflict:
                    return Conflict(response);
                case HttpStatusCode.Forbidden:
                    return StatusCode((int)HttpStatusCode.Forbidden, response);
                default:
                    return Ok(response);
            }
        }
    }
}
