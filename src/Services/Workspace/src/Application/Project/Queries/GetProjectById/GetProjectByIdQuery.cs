using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Queries.GetProjectById
{
    public class GetProjectByIdQuery :IRequest<Core.Entities.Project>
    {
        public Guid Id { get; set; }
 
    }
}
