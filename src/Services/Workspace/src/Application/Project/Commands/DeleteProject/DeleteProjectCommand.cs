using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Commands.DeleteProject
{
    public class DeleteProjectCommand:IRequest<DeleteProjectDto>
    {
        public Guid ProjectId { get; set; }
    }
}
