using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Collaboration.ShareDocs.Application.Project.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<string>
    {
        public string Label { get; set; }
        public string Description { get; set; }
        public Guid WorkspaceId { get; set; }

    }
}
