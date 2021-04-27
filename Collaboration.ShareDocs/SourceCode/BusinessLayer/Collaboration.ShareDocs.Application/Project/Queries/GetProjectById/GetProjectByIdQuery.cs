using System;
using MediatR;

namespace Collaboration.ShareDocs.Application.Project.Queries.GetProjectById
{
    public class GetProjectByIdQuery :IRequest<Collaboration.ShareDocs.Persistence.Entities.Project>
    {
        public Guid Id { get; set; }
 
    }
}
