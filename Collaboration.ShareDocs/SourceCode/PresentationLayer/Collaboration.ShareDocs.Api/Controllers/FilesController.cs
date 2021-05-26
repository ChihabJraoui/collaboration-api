﻿using Collaboration.ShareDocs.Application.Commands.Files;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public async Task<IActionResult> Create(CreateFileCommand command)
        {
            var result = await this.Mediator.Send(command);
            return FormatResponseToActionResult(result);
        }
        [HttpPost]
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
                    var stream = new FileStream(fullPath, FileMode.Create);
                    using (stream) 
                    {
                        await file.CopyToAsync(stream);
                    }
                    return  Ok( new { dbPath });
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
        [HttpGet("id")]
        public async Task<IActionResult> GetByProjectId(Guid id)
        {
            var result = await this.Mediator.Send(new GetFilesByFolderId { FolderParentId = id });
            return FormatResponseToActionResult(result);
        }
    }
}
