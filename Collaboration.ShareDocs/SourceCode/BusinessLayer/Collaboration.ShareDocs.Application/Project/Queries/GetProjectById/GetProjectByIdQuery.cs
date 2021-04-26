using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Queries.GetProjectById
{
    public class GetProjectByIdQuery :IRequest<Collaboration.ShareDocs.Persistence.Entities.Project>
    {
        public Guid Id { get; set; }
 
    }
}
