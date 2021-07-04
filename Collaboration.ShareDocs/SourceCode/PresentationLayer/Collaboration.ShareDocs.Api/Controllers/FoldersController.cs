using Collaboration.ShareDocs.Application.Commands.Folders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    [Authorize]
    [Route("api/folders")]
    public class FoldersController:BaseController
    {

        /// <summary>
        /// Get Folder by Id
        /// </summary>
        /// <param name="FolderId">GetFolderCommand</param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetByFilter([FromQuery] GetFolderFilterCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Get Folder by Id
        /// </summary>
        /// <param name="FolderId">GetFolderCommand</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{folderId:Guid}")]
        public async Task<IActionResult> GetFolderDetails([FromRoute] Guid folderId)
        {
            var result = await this.Mediator.Send(new GetFolderCommand { FolderId = folderId });
            return FormatResponseToActionResult(result);
        }

        /// <summary>
        /// Create new Folder
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
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
        [Route("")]
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
        [Route("")]
        public async Task<IActionResult> Delete(DeleteFolderCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
    }
}
