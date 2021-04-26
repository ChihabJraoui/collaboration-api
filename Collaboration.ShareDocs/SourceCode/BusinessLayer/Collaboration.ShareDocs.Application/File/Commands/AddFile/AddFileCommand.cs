using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.File
{
    public class AddFileCommand:IRequest<Core.Entities.File>
    {
        public string Name { get; set; }
        public Guid FolderParentId { get; set; }
        public string PathFile { get; set; }
    }
}
