using Collaboration.ShareDocs.Application.Commands.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    public class UsersController:BaseController
    {
        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="UserId">GetUserCommand</param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await this.Mediator.Send(new GetUserCommand { UserId = id });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteUserCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
    }
}
