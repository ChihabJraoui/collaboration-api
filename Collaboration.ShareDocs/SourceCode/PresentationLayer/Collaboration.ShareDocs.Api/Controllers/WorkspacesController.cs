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
        [HttpPost("create")]
        public async Task<ActionResult<WorkspaceDto>> Create(CreateWorkspaceCommand command)
        {
            var result = await this.Mediator.Send(command);
            return result;
        }
       
        /// <summary>
        /// Update Workspace
        /// </summary>
        /// <param name="command"> new </param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<ActionResult<WorkspaceDto>> Update(UpdateWorkspaceCommand command)
        {
            var result = await this.Mediator.Send(command);
            return result;
        }



    }
}
