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
        /// Get User by username
        /// </summary>
        /// <param name="Username">GetUserCommand</param>
        /// <returns></returns>
        [HttpGet("username")]
        public async Task<IActionResult> Get(string username)
        {
            var result = await this.Mediator.Send(new GetUserCommand { Username = username });
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// Get Users by keyword
        /// </summary>
        /// <param name="UserId">GetUsersCommand</param>
        /// <returns>string</returns>
        [HttpGet("keyword")]
        public async Task<IActionResult> GetByKyword(string keyword)
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
