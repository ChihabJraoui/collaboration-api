using System;

namespace Collaboration.ShareDocs.Application.Project.Commands.DeleteProject
{
    public class DeleteProjectDto
    {
        public Guid ProjectId { get; set; }
        public string Label { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
