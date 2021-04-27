using FluentValidation;

namespace Collaboration.ShareDocs.Application.Project.Commands.UpdateProject
{
    public class UpdateProjectCommandHandlerValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandHandlerValidator()
        {
            this.RuleFor(x => x.Label).NotEmpty().MinimumLength(3).WithMessage("Label length should be at least three character");
            this.RuleFor(x => x.Description).NotEmpty().WithMessage("Description is empty");
        }
    }
}
