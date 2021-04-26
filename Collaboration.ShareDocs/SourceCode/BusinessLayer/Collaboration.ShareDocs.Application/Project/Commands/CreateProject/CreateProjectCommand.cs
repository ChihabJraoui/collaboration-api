using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Commands
{
    public class CreateProjectCommand : IRequest<string>
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public Guid WorkspaceId { get; set; }

    }
}
