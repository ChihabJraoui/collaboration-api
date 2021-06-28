using Collaboration.ShareDocs.Application.Commands.Files;
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
    public class FilesController:BaseController
    {
        /// <summary>
        /// Create new File
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(CreateFileCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> UploadFile()
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
                    var extension = System.IO.Path.GetExtension(file.FileName);
                    var stream = new FileStream(fullPath, FileMode.Create);
                    using (stream) 
                    {
                        await file.CopyToAsync(stream);
                    }
                    return  Ok( new { dbPath , extension});
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

        /// <summary>
        /// Get Files by folder Id
        /// </summary>
        /// <param name="FolderId">GetFilesByFolderId</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetByProjectId(Guid id)
        {
            var result = await this.Mediator.Send(new GetFilesByFolderId { FolderParentId = id });
            return FormatResponseToActionResult(result);
        }


        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<IActionResult> GetByCreatedBy(Guid userId)
        {
            var result = await this.Mediator.Send(new GetFilesCreatedBy { UserId = userId });
            return FormatResponseToActionResult(result);
        }
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetfollowingFiles()
        {
            var result = await this.Mediator.Send(new GetFollowingFiles());
            return FormatResponseToActionResult(result);
        }
        [HttpGet]
        [Route("{filePath}/{fileName}")]
        public ActionResult DownloadDocument(string filePath,string fileName)
        {

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);

        }
    }
}
