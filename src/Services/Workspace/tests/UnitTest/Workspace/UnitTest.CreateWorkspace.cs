using Application.Common.Exceptions;
using Application.Workspace.Commands;
using Application.Workspace.Commands.CreateWorkspace;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace UnitTest.Workspace
{
    public class ApplicationTest:BaseMock
    {
        [Fact]
        public async void ValidCommand_SouldReturneWorkspaceGuid()
        {
            //Arrange
            var command = new CreateWorkspaceCommand
            {
                Name="TEAM G",
                Description="working on ai project",
                Image="lena.jpg"
            };
            this._methodesRepositoryMock.Setup(y=>y.UniqueName(command.Name, new System.Threading.CancellationToken())).ReturnsAsync(true);
            this._workspaceRepositoryMock.Setup(x => x.CreateAsync(command, _currentUserMock.Object))
                .ReturnsAsync(Guid.NewGuid());

            //Act
            var handler = new CreateWorkspaceHandler(_workspaceRepositoryMock.Object,
                                                     _currentUserMock.Object,
                                                     _methodesRepositoryMock.Object);
            var workspaceId = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.NotEqual(workspaceId,Guid.Empty);
        }

        [Fact]
        public async void InValidCommand_ShouldReturnErrorMessage()
        {
            // Arrange
            var command = new CreateWorkspaceCommand
            {
                Name = "",
                Description = "",
                Image = ""
            };

            this._workspaceRepositoryMock.Setup(x => x.CreateAsync(command, _currentUserMock.Object))
                .ReturnsAsync(Guid.NewGuid());

            // Act
            var handler = new CreateWorkspaceHandler(_workspaceRepositoryMock.Object,
                                                     _currentUserMock.Object,
                                                     _methodesRepositoryMock.Object);
            // Assert
            await Assert.ThrowsAsync<BusinessRuleException>(
                () => handler.Handle(command, new System.Threading.CancellationToken()));
        }

        [Fact]
        public async void WorkspaceAlreadyExist_ShouldThrowException()
        {
            //Arrange
            var command = new CreateWorkspaceCommand
            {
                Name = "TEAM G",
                Description = "working on ai project",
                Image = "lena.jpg"
            };
            this._methodesRepositoryMock.Setup(x => x.UniqueName(command.Name, new System.Threading.CancellationToken()))
                .ReturnsAsync(false);

            // Act
            var handler = new CreateWorkspaceHandler(_workspaceRepositoryMock.Object,
                                                     _currentUserMock.Object,
                                                     _methodesRepositoryMock.Object);
            // Assert
            await Assert.ThrowsAsync<BusinessRuleException>(
                () => handler.Handle(command, new System.Threading.CancellationToken()));
        }
    }
}
