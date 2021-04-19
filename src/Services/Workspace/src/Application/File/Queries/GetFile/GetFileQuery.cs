using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.File.Queries.GetFile
{
    public class GetFileQuery:IRequest<Core.Entities.File>
    {
        public Guid FileId { get; set; }
    }
}
