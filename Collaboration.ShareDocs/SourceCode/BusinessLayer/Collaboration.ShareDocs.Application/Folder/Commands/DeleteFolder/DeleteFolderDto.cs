using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Folder.Commands.DeleteFolder
{
    public class DeleteFolderDto
    {
        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
