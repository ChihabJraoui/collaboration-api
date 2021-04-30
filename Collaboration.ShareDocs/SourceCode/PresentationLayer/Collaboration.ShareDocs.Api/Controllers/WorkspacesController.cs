using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Application.Commands.Workspaces;
using Collaboration.ShareDocs.Application.Commands.Workspaces.Dto;

namespace Collaboration.ShareDocs.Api.Controllers
{

    public class WorkspacesController : BaseController
    { 
 
        /// <summary>
        /// Create new Workspace
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkspaceCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
       
        /// <summary>
        /// Update Workspace
        /// </summary>
        /// <param name="command"> new </param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateWorkspaceCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// Delete Workspace
        /// </summary>
        /// <param name="command">  </param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteWorkspaceCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }



    }
}
