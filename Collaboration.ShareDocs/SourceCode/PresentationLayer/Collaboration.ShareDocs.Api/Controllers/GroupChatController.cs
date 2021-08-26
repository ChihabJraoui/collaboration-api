using Collaboration.ShareDocs.Application.Commands.GroupChat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/GroupChat")]
    public class GroupChatController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateGroup(CreateGroupCommand command)
        {
            var result = await this.Mediator.Send(new CreateGroupCommand { Name = command.Name });
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("sendMessage")]
       
        public async Task<IActionResult> MessageGroup(GroupChatCommand command)
        {
            var result = await this.Mediator.Send(new GroupChatCommand { GroupId = command.GroupId,Message= command.Message });
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("joinGroup")]

        public async Task<IActionResult> JoinGroup(JoinGroupCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
    }
}
