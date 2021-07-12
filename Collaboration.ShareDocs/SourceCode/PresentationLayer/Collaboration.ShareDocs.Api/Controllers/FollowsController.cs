using Collaboration.ShareDocs.Application.Commands.Follows;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    [Authorize]
    [Route("api/follows")]
    public class FollowsController:BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("{userId:Guid}/followers")]
        //public async Task<IActionResult> GetFollowers(Guid userId)
        //{
        //    var result = await this.Mediator.Send(new GetFollowersCommand { UserId = userId });
        //    return FormatResponseToActionResult(result);
        //}

        /// <summary>
        /// Get followings
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("{userId:Guid}/followings")]
        //public async Task<IActionResult> GetFollowings(Guid userId)
        //{
        //    var result = await this.Mediator.Send(new GetFollowingsCommand { UserId = userId });
        //    return FormatResponseToActionResult(result);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(AddFollowCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpGet]
        //[Route("is-following/{userId:Guid}")]
        //public async Task<IActionResult> IsFollowing(Guid userId)
        //{
        //    var result = await this.Mediator.Send(new IsFollowingCommand() { UserId = userId } );
        //    return FormatResponseToActionResult(result);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        //[HttpDelete]
        //[Route("{followingId:Guid}")]
        //public async Task<IActionResult> Unfollow(Guid followingId)
        //{
        //    var result = await this.Mediator.Send(new UnfollowCommand() { FollowingId= followingId });
        //    return FormatResponseToActionResult(result);
        //}
    }
}
