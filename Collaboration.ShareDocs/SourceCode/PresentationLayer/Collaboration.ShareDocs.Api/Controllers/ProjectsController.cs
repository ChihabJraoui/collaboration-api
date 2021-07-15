using Collaboration.ShareDocs.Application.Commands.Projects;
using Collaboration.ShareDocs.Application.Commands.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{

    [Route("api/projects")]
    public class ProjectsController:BaseController
    {

        /// <summary>
        /// Get Projects By WorkspaceId
        /// </summary>
        /// <param name="">GetProjectsByWorkspaceIdCommand</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetManyProjects([FromQuery] GetProjectsFilterCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Project by Id
        /// </summary>
        /// <param name="ProjectId">GetProjectCommand</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{projectId:Guid}")]
        public async Task<IActionResult> GetProjectDetails([FromRoute] Guid projectId)
        {
            var result = await this.Mediator.Send(new GetProjectCommand { ProjectId = projectId });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Create new Workspace
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
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
        [Route("")]
        public async Task<IActionResult> Update( UpdateProjectCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Delete Project
        /// </summary>
        /// <param name="command">  </param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{projectId:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid projectId)
        {
            var result = await this.Mediator.Send(new DeleteProjectCommand { ProjectId = projectId });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Projects By keyword
        /// </summary>
        /// <param name="">GetProjectsByKeywordCommand</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byKeyword")]
        public async Task<IActionResult> GetByKeyword(string keyword)
        {
            var result = await this.Mediator.Send(new GetProjectsByKeywordCommand() { Keyword = keyword });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Projects By Created User
        /// </summary>
        /// <param name="">GetProjectsByCreatedUserCommand</param>
        /// <returns></returns>
        [HttpGet]
        [Route("byUser/{userId:Guid}")]
        public async Task<IActionResult> GetByCreatedUser([FromRoute] Guid userId)
        {
            var result = await this.Mediator.Send(new GetProjectsByCreatedUserCommand() { UserId = userId });
            return FormatResponseToActionResult(result);
        }


        /// <summary>
        /// Get Projects By Created User
        /// </summary>
        /// <param name="">GetProjectsByCreatedUserCommand</param>
        /// <returns></returns>
        [HttpPost]
        [Route("addUsers")]
        public async Task<IActionResult> AddUsersToProject(AddUserToProject command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getUsers/{projectId:Guid}")]
        public async Task<IActionResult> GetUsers([FromRoute]Guid projectId)
        {
            var result = await this.Mediator.Send(new GetUsersNotBelongsToProject { ProjectId = projectId});
            return FormatResponseToActionResult(result);
        }


    }
}
