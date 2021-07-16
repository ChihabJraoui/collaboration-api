using Collaboration.ShareDocs.Application.Commands.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    [Authorize]
    [Route("api/users")]
    public class UsersController:BaseController
    {
        
        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            var result = await this.Mediator.Send(new GetUserById { UserId = userId});
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// Get Users by keyword
        /// </summary>
        /// <param name="UserId">GetUsersCommand or Get All Users</param>
        /// <returns>string</returns>
        [HttpGet]
        [Route("{keyword}")]
        public async Task<IActionResult> GetByKyword([FromRoute] string keyword)
        {
            var result = await this.Mediator.Send(new GetUsersByKeyWordCommand { Keyword = keyword });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
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
        [Route("")]
        public async Task<IActionResult> Delete(DeleteUserCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
    }
}
