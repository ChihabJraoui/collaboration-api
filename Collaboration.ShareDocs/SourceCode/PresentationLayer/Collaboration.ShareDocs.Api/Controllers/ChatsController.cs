using Collaboration.ShareDocs.Application.Commands.IndividualChat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    [Authorize]
    [Microsoft.AspNetCore.Mvc.Route("api/Chats")]
    public class ChatsController:BaseController
    {
        /// <summary>
        /// Create new Folder
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
      
        public async Task<IActionResult> Create(IndividualChatCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

    }
}
