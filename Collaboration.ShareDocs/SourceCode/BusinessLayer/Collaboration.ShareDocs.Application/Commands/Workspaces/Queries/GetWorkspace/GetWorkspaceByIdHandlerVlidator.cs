using FluentValidation;

namespace Collaboration.ShareDocs.Application.Workspace.Queries.GetWorkspace
{
    public class GetWorkspaceByIdHandlerVlidator:AbstractValidator<GetWorkspaceByIdQuery>
    {
        public GetWorkspaceByIdHandlerVlidator()
        {
            this.RuleFor(x => x.WorkspaceRequestId).NotEmpty().WithMessage("Id is required");
        }
    }
}
