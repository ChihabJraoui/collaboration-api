using FluentValidation;

namespace Collaboration.ShareDocs.Application.Project.Commands.CreateProject
{
    public class CreateProjectHandlerValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectHandlerValidator()
        {
            this.RuleFor(p => p.Label).NotEmpty().MinimumLength(3).WithMessage("Name length should be at least three character");

            this.RuleFor(p => p.WorkspaceId).NotEmpty().WithMessage("This new Project should belong to an existing workspace.");

        }
    }
}
