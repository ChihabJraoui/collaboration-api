using Application.Common.Exceptions;
using Application.Workspace.Commands.DeleteWorkspace;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest.Workspace
{
    public class UnitTestDeleteWorkspace: BaseMock
    {
        [Fact]
        public async void InvalidGuidWorkspace_ShouldRetournException()
        {
            // Arrange
            var request = new DeleteWorkspaceCommad
            {
                WorkspaceId = new Guid()
            };
            var workspace = new Core.Entities.Workspace { };
            var workspacedto = new DeleteWorkspaceReturnDto { };
            _workspaceRepositoryMock.Setup(y => y.DeleteAsync(request, _currentUserMock.Object, new System.Threading.CancellationToken())).ReturnsAsync(workspacedto);

            //Act
            var handler = new DeleteWorkspaceHandler(_workspaceRepositoryMock.Object, _currentUserMock.Object);

            //Assert
            await Assert.ThrowsAsync<BusinessRuleException>(
                 () => handler.Handle(request, new System.Threading.CancellationToken()));
        }
    }
}
