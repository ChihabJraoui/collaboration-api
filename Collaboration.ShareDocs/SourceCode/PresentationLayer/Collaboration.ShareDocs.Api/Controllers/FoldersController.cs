using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    public class FoldersController:ApiController
    {
        private IMediator _mediator;

        public FoldersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create new Project
        /// </summary>
        /// <param name="command"> CreateProjectCommand </param>
        /// <returns>string</returns>
        //[HttpPost("create")]
        //public async Task<ActionResult<Folder>> Create(AddFolderCommand command)
        //{
        //    var result = await this._mediator.Send(command);
        //    return result;
        //}
        /// <summary>
        /// Get Project
        /// </summary>
        /// <param name="query"> GetProjectByIdQuery</param>
        /// <returns>Project</returns>
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Folder>> Get(Guid id)
        //{
        //    var result = await this._mediator.Send(new GetFolderQuery {FolderId=id});
        //    return result;
        //}

        /// <summary>
        /// Get all Folders
        /// </summary>
        /// <param name="query">GetAllFoldersQuery</param>
        /// <returns>GetFoldersDtoLists</returns>
        //[HttpGet("getAll")]
        //public async Task<ActionResult<GetFoldersDtoLists>> GetAll([FromQuery] GetAllFoldersQuery query)
        //{
        //    var result = await this._mediator.Send(query);
        //    return result;
        //}

        /// <summary>Rename Folder
        /// </summary>
        /// <param name="command"> </param>
        /// <returns></returns>
        //[HttpPut]
        //public async Task<ActionResult<string>> Update(RenameFolderCommand command)
        //{
        //    var result = await this._mediator.Send(command);
        //    return result;
        //}

        /// <summary>
        /// Delete Folder
        /// </summary>
        /// <param name="command"> Delete </param>
        /// <returns>DeleteFolderDto</returns>
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<DeleteFolderDto>> Delete(Guid id)
        //{
        //    var result = await this._mediator.Send(new DeleteFolderCommand {FolderId=id });
        //    return result;
        //}

    }
}
