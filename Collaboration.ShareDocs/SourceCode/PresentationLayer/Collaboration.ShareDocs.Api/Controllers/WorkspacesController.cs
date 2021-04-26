using Application.Workspace.Commands;
using Application.Workspace.Commands.DeleteWorkspace;
using Application.Workspace.Commands.UpdateWorkspace;
using Application.Workspace.Queries.GetAllWorkspaces;
using Application.Workspace.Queries.GetLastModifiedWorkspace;
using Application.Workspace.Queries.GetLastWorkspace;
using Application.Workspace.Queries.GetWorkspace;
using Application.Workspace.Queries.GetWorkspaceByKeyWord;
using Application.Workspace.Queries.GetWorkspacesCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


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

        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<Guid>> Create(CreateWorkspaceCommand command)
        {
            var result = await this._mediator.Send(command);
            return result;
        }


        /// <summary>
        /// Get Workspace by Id
        /// </summary>
        /// <param name="id">GetWorkspaceByIdQuery</param>
        /// <returns></returns>
        [HttpGet("id")]
        [Authorize(Roles = "SuperAdmin")]

        public async Task<ActionResult<Core.Entities.Workspace>> Get(Guid id)
        {
            var result = await this._mediator.Send(new GetWorkspaceByIdQuery { WorkspaceRequestId = id }) ;
            return result;
        }

        /// <summary>
        /// Get all Workspaces 
        /// </summary>
        /// <param name="query">GetAllWorkspaceQuery</param>
        /// <returns>WorkspacesDTOLists</returns>
        [HttpGet("getAll")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<WorkspacesDTOLists>> GetAll([FromQuery] GetAllWorkspaceQuery query)
        {
            var result = await this._mediator.Send(query);
            return result;
        }
        /// <summary>
        /// Get last workspace 
        /// </summary>
        /// <param name="query">GetLastWorkspaceQuery</param>
        /// <returns>WorkspacesDTOLists</returns>
        [HttpGet("lastCreatedworkspace")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<LastWorkspaceDto>> GetLast([FromQuery] GetLastWorkspaceQuery query)
        {
            var result = await this._mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Get last Modified workspace 
        /// </summary>
        /// <param name="query">GetLastModifiedWorkspaceQuery</param>
        /// <returns>WorkspacesLastModifiedDto</returns>
        [HttpGet("lastModifiedworkspace")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<WorkspaceLastModifiedDto>> GetLastModified([FromQuery] GetLastModifiedWorkspaceQuery query)
        {
            var result = await this._mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Get workspaces count
        /// </summary>
        /// <param name="query">GetWorkspacesCountQuery</param>
        /// <returns>int</returns>
        [HttpGet("getCount")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<int>> GetCount([FromQuery] GetWorkspacesCountQuery query)
        {
            var result = await this._mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Get Workspaces By Key word 
        /// </summary>
        /// <param name="query">GetAllWorkspaceQuery</param>
        /// <returns>WorkspacesDTOLists</returns>
        [HttpGet("search")]
        public async Task<ActionResult<WorkspaceDtoLists>> GetByKeyWord([FromQuery] GetWorkspaceByKeyWord query)
        {
            var result = await this._mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Update Workspace
        /// </summary>
        /// <param name="command"> new </param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<Core.Entities.Workspace>> Update(UpdateWorkspaceCommand command)
        {
           
           
            var result =await this._mediator.Send(command);
                return result;
            
        }

        /// <summary>
        /// Delete Workspace
        /// </summary>
        /// <param name="command"> Delete </param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult<DeleteWorkspaceReturnDto>> Delete(Guid id)
        {
             var result = await this._mediator.Send(new DeleteWorkspaceCommad { WorkspaceId = id });
            return result;
        }

    }
}
