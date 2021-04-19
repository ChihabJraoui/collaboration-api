using Application.Workspace.Commands.UpdateWorkspace;
using Application.Workspace.Queries.GetWorkspace;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest.Workspace
{
    public class UpdateWorkspaceTests:BaseMock
    {
        [Fact]
        public async void ValidRequest_SouldChangeWorkspace()
        {
            //Arrange
            var command = new UpdateWorkspaceCommand
            {
                WorkspaceId = Guid.Parse("EBEBFFFF-E5C0-4558-C2DA-08D854A2FCDE"),
                Name = "TEiiii",
                Description = "working on ai project",
                Image = "lena.jpg",
                IsPrivate = true,
                BookMark = false
            };
            var workspaceCommand = new Core.Entities.Workspace
            {
                Id = command.WorkspaceId,
                Name = command.Name,
                Description = command.Description,
                Image = command.Image,
                IsPrivate = command.IsPrivate,
                BookMark = command.BookMark
            };
            var workspaceexistdatabase = new Core.Entities.Workspace
            {
                Id = Guid.Parse("EBEBFFFF-E5C0-4558-C2DA-08D854A2FCDE"),
                Name = "TEAM P",
                Description = "working on ai project",
                Image = "lena.jpg",
                IsPrivate = false,
                BookMark = true
            };
            //var queryId = new GetWorkspaceByIdQuery { WorkspaceRequestId = command.WorkspaceId } ;
            this._workspaceRepositoryMock.Setup(y => y.GetAsync(It.IsAny<GetWorkspaceByIdQuery>())).ReturnsAsync(workspaceexistdatabase);
            this._methodesRepositoryMock.Setup(x => x.UniqueName(command.Name, new System.Threading.CancellationToken())).ReturnsAsync(true);
            this._workspaceRepositoryMock.Setup(z => z.UpdataAsync(command, _currentUserMock.Object, new System.Threading.CancellationToken())).ReturnsAsync(workspaceCommand);

            //Act
            var handler = new UpdateWorkspaceHandler(_workspaceRepositoryMock.Object,_methodesRepositoryMock.Object, _currentUserMock.Object);
            var workspaceUpdated = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.Equal(workspaceCommand, workspaceUpdated);
        }
    }
}
