using Collaboration.ShareDocs.Application.Commands.Projects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    public class ProjectsController:BaseController
    {
        /// <summary>
        /// Create new Workspace
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Update project
        /// </summary>
        /// <param name="command"> new </param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProjectCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Project by Id
        /// </summary>
        /// <param name="ProjectId">GetProjectCommand</param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await this.Mediator.Send(new GetProjectCommand { ProjectId = id });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="command">  </param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProjectCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Projects By keyword
        /// </summary>
        /// <param name="">GetProjectsByKeywordCommand</param>
        /// <returns></returns>
        [HttpGet("keyword")]
        public async Task<IActionResult> GetByKeyword(string keyword)
        {
            var result = await this.Mediator.Send(new GetProjectsByKeywordCommand() { Keyword = keyword });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Projects By WorkspaceId
        /// </summary>
        /// <param name="">GetProjectsByWorkspaceIdCommand</param>
        /// <returns></returns>
        [HttpGet("workspaceId")]
        public async Task<IActionResult> GetByWorkspaceId(Guid workspaceId)
        {
            var result = await this.Mediator.Send(new GetProjectsCommandByWorkspaceId() { WorkspaceId = workspaceId });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Projects By Created User
        /// </summary>
        /// <param name="">GetProjectsByCreatedUserCommand</param>
        /// <returns></returns>
        [HttpGet("userId")]
        public async Task<IActionResult> GetByCreatedUser(Guid userId)
        {
            var result = await this.Mediator.Send(new GetProjectsByCreatedUserCommand() { UserId = userId });
            return FormatResponseToActionResult(result);
        }
    }
}
