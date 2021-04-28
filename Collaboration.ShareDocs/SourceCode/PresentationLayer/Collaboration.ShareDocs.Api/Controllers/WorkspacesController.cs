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

    public class WorkspacesController : ApiController
    {
        private readonly IMediator _mediator;

        public WorkspacesController(IMediator meditor)
        {
            this._mediator = meditor;
        }
        /// <summary>
        /// Create new Workspace
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<ActionResult<CreateWorkspaceDto>> Create(CreateWorkspaceCommand command)
        {
            var result = await this._mediator.Send(command);
            return result;
        }


        

    }
}
