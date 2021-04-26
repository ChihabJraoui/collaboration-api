using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest
    {
        public Guid ProjectId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
