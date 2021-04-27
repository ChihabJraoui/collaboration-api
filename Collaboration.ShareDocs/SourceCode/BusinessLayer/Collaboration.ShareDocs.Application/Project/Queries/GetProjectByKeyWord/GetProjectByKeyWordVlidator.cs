using FluentValidation;

namespace Collaboration.ShareDocs.Application.Project.Queries.GetProjectByKeyWord
{
    public class GetProjectByKeyWordVlidator:AbstractValidator<GetProjectByKeyWordQuery>
    {
        public GetProjectByKeyWordVlidator()
        {
            this.RuleFor(x => x.KeyWord).NotEmpty().MinimumLength(3).WithMessage("Is required");
        }
    
    }
}
