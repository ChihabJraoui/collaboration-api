using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Api.Controllers
{
    public class FilesController:ApiController
    {
        private IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        

        /// <summary>
        /// Create new Project
        /// </summary>
        /// <param name="command"> CreateProjectCommand </param>
        /// <returns>string</returns>
        //[HttpPost("uplod")]
        
        //public async Task<ActionResult<File>> Post(AddFileCommand command)
        //{
        //    var result = await this._mediator.Send(command);
        //    return result;
        //}



        //[HttpGet("{id}")]
        //public async Task<ActionResult<File>> Get(Guid id)
        //{
        //    var result = await this._mediator.Send(new GetFileQuery { FileId=id });
        //    return result;
        //}
        //
        [HttpPost]
        public IActionResult UploadFile()
        {

            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"internal server error:{ex}");
            }
        }
    }
}
