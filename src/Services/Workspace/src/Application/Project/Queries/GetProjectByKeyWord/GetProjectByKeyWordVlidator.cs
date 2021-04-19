using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Project.Queries.GetProjectByKeyWord
{
    public class GetProjectByKeyWordVlidator:AbstractValidator<GetProjectByKeyWordQuery>
    {
        public GetProjectByKeyWordVlidator()
        {
            this.RuleFor(x => x.KeyWord).NotEmpty().MinimumLength(3).WithMessage("Is required");
        }
    
    }
}
