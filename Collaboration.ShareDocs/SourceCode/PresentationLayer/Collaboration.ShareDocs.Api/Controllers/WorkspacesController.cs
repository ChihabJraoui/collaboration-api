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
    [Authorize]
    [Route("api/workspaces")]
    public class WorkspacesController : BaseController
    {

        /// <summary>
        /// Get many workspaces
        /// </summary>
        /// <param name="">GetWorkspacesCommand</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAll([FromQuery] string keyword )
        {
            var result = await this.Mediator.Send(new GetWorkspacesCommand {Keyword = keyword });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Workspace by Id
        /// </summary>
        /// <param name="WorkspaceId">GetWorkspaceCommand</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{workspaceId:Guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid workspaceId)
        {
            var result = await this.Mediator.Send(new GetWorkspaceCommand { WorkspaceId = workspaceId });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Create new Workspace
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
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
        [Route("{workspaceId:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid workspaceId, UpdateWorkspaceCommand command)
        {
            command.WorkspaceId = workspaceId;
            var result = await this.Mediator.Send(command);

            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Delete Workspace
        /// </summary>
        /// <param name="command">  </param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{workspaceId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid workspaceId)
        {
            var result = await this.Mediator.Send(new DeleteWorkspaceCommand { WorkspaceId = workspaceId });
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// Get Last Modified Workspace
        /// </summary>
        /// <param name="">GetLastModifiedWorkspace</param>
        /// <returns></returns>
        [HttpGet]
        [Route("last-modified")]
        public async Task<IActionResult> GetLastModified()
        {
            var result = await this.Mediator.Send(new GetLastModifiedWorkspace());
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// Get Last Modified Workspace
        /// </summary>
        /// <param name="">GetLastModifiedWorkspace</param>
        /// <returns></returns>
        [HttpGet]

        [Route("last-created")]
        public async Task<IActionResult> GetLastCreated()
        {
            var result = await this.Mediator.Send(new GetLastCreatedWorkspace());
            return FormatResponseToActionResult(result);
        }
       

        /// <summary>
        /// Get Number of workspaces
        /// </summary>
        /// <param name="">GetLastModifiedWorkspace</param>
        /// <returns></returns>
        [HttpGet]
        [Route("count")]
        public async Task<IActionResult> GetCount()
        {
            var result = await this.Mediator.Send(new GetWorkspacesCount());
            return FormatResponseToActionResult(result);
        }






    }
}
