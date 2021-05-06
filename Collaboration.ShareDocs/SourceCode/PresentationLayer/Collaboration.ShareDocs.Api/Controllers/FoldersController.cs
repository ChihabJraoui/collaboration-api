using Collaboration.ShareDocs.Application.Commands.Folders;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    public class FoldersController:BaseController
    {
        /// <summary>
        /// Create new Folder
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateFolderCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Rename folder
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Rename(UpdateFolderCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// Rename folder
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteFolderCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// Get Folder by Id
        /// </summary>
        /// <param name="FolderId">GetFolderCommand</param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await this.Mediator.Send(new GetFolderCommand { FolderId = id });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Folder by Id
        /// </summary>
        /// <param name="FolderId">GetFolderCommand</param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> GetByProjectId(Guid id)
        {
            var result = await this.Mediator.Send(new GetFoldersByProjectId { ProjectId = id });
            return FormatResponseToActionResult(result);
        }
        /// <summary>
        /// Get Folder by UserId
        /// </summary>
        /// <param name="UserId">GetFolderByUserIdCommand</param>
        /// <returns></returns>
        [HttpGet("id")]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var result = await this.Mediator.Send(new GetFoldersByCreatedUserCommand { UserId = id });
            return FormatResponseToActionResult(result);
        }
    }
}
