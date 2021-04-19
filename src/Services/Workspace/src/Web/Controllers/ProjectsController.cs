using Application.Project.Commands;
using Application.Project.Commands.AddMemberToProject;
using Application.Project.Commands.DeleteProject;
using Application.Project.Commands.UpdateProject;
using Application.Project.Queries.GetProjectById;
using Application.Project.Queries.GetProjectByKeyWord;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class ProjectsController:ApiController
    {
        private IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new Project
        /// </summary>
        /// <param name="command"> CreateProjectCommand </param>
        /// <returns>string</returns>
        [HttpPost("create")]
        public async Task<ActionResult<string>> Create(CreateProjectCommand command)
        {
            var result = await this._mediator.Send(command);
            return result;
        }

        /// <summary>
        /// Get Project
        /// </summary>
        /// <param name="query"> GetProjectByIdQuery</param>
        /// <returns>Project</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Core.Entities.Project>> GetById(Guid id)
        {
            var result = await this._mediator.Send(new GetProjectByIdQuery { Id=id });
            return result;
        }
        /// <summary>
        /// Seach Project
        /// </summary>
        /// <param name="query"> GetProjectByKeyWordQuery</param>
        /// <returns>Projects</returns>
        [HttpGet("search")]
        public async Task<ProjectsDTOLists> GetByKeyWord([FromQuery] GetProjectByKeyWordQuery query)
        {
            var result = await this._mediator.Send(query);
            return result;
        }

        /// <summary>
        /// Update Project
        /// </summary>
        /// <param name="command"> </param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> Update(UpdateProjectCommand command)
        {
             await this._mediator.Send(command);
             return NoContent();
        }
        /// <summary>
        /// Delete Workspace
        /// </summary>
        /// <param name="command"> Delete </param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteProjectDto>> Delete(Guid id)
        {
            var result = await this._mediator.Send(new DeleteProjectCommand {ProjectId = id } );
            return result;
        }

        [HttpPost("addMember")]
        public async Task<ActionResult<Unit>> Add(AddMemberToProject command)
        {
             var result = await this._mediator.Send(command);
            return result;
        }
    }
}
