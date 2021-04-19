using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Commands.DeleteProject
{
    public class DeleteProjectDto
    {
        public Guid ProjectId { get; set; }
        public string Label { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
