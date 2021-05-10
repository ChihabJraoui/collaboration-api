using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Application.Commands.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Collaboration.ShareDocs.Api.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : BaseController
    {
        [
            AllowAnonymous,
            HttpPost("Register")
        ]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await this.Mediator.Send(command);
            if (result.StatusCode == (int)HttpStatusCode.BadRequest)
            {
                return this.BadRequest(result);
            }

            if (result.StatusCode == (int)HttpStatusCode.NotFound)
            {
                return this.NotFound(result);
            }

            return this.Ok(result.Object);
        }

        //[
        //    AllowAnonymous,
        //    HttpPost("RegisterWithInvitation")
        //]
        //public async Task<IActionResult> RegisterWithInvitation(RegisterWithInvitationCommand command)
        //{
        //    var result = await this.Mediator.Send(command);
        //    if (result.StatusCode == (int)HttpStatusCode.BadRequest)
        //    {
        //        return this.BadRequest(result);
        //    }

        //    if (result.StatusCode == (int)HttpStatusCode.NotFound)
        //    {
        //        return this.NotFound(result);
        //    }

        //    return this.Ok(result);
        //}

        [
            AllowAnonymous,
            HttpPost("Login")
        ]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await this.Mediator.Send(command);
            return this.FormatResponseToActionResult(result);
        }

        [
            AllowAnonymous,
            HttpPost("RefreshToken")
        ]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var result = await this.Mediator.Send(command);
            return this.FormatResponseToActionResult(result);
        }
    }
}