using Application.Common.Exceptions;
using Application.Project.Commands;
using Application.Workspace.Queries.GetWorkspace;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.Workspace;
using Xunit;

namespace UnitTest.Project
{
    public class UnitTest:BaseMock
    {
        [Fact]
        public async void ValidCommand_ShouldReturnProjectName()
        {
            //Arrage
            var command = new CreateProjectCommand
            {
                Label = "Plateforme",
                Description = "abcdefghigklmopqrstuvwxyz",
                WorkspaceId = Guid.Parse("CBFA66E8-DF6F-4EC2-78B1-08D86535018F")
            };

            var workspace = new Core.Entities.Workspace { 
                Name = "testWorkspaceMock",
                Description = "abcdefghigklmopqrstuvwxyz",
                Id=command.WorkspaceId
            };
            
            this._workspaceRepositoryMock.Setup(w => w.GetAsync(It.IsAny<GetWorkspaceByIdQuery>())).ReturnsAsync(workspace);
            this._methodesRepositoryMock.Setup(u => u.UniqueName(command.Label, new System.Threading.CancellationToken())).ReturnsAsync(true);
            this._projectRepositoryMock.Setup(x => x.CreateAsync(command,workspace, _currentUserMock.Object)).ReturnsAsync(command.Label);
           

            //Act
            var handler = new CreateProjectHandler(_methodesRepositoryMock.Object, _projectRepositoryMock.Object, _currentUserMock.Object, _workspaceRepositoryMock.Object);
            var projectLabel = await handler.Handle(command, new System.Threading.CancellationToken());

            //Assert 
            Assert.NotNull(projectLabel);
        }


        [Fact]
        public async void IfWorkspaceDoesntExist_ShouldThrowEXception()
        {
            //Arrage
            var command = new CreateProjectCommand
            {
                Label = "PlateformeShouldThrow",
                Description = "abcdefghigklmopqrstuvwxyz",
                WorkspaceId = Guid.Parse("CBFA88E8-DF6F-4EC2-78B1-08D86535018F")

            };
            var workspace = new Core.Entities.Workspace
            {
               
            };
            var query = new GetWorkspaceByIdQuery { WorkspaceRequestId = command.WorkspaceId };
            this._workspaceRepositoryMock.Setup(w => w.GetAsync(query)).ReturnsAsync(workspace);
            this._projectRepositoryMock.Setup(p => p.CreateAsync(command, workspace, _currentUserMock.Object));

            //Act
            var handler = new CreateProjectHandler(_methodesRepositoryMock.Object, _projectRepositoryMock.Object, _currentUserMock.Object, _workspaceRepositoryMock.Object);
            //Asset
            await Assert.ThrowsAsync<BusinessRuleException>(
                 () => handler.Handle(command, new System.Threading.CancellationToken()));

        }

        [Fact]
        public async void ProjectLabelAlreadyExist_ShouldThrowException()
        {
            //Arrange
            var command = new CreateProjectCommand
            {
                Label = "PlateformeShouldThrow",
                Description = "abcdefghigklmopqrstuvwxyz",
                WorkspaceId = Guid.Parse("CBFA88E8-DF6F-4EC2-78B1-08D86535018F")
            };
            this._methodesRepositoryMock.Setup(x => x.UniqueName(command.Label, new System.Threading.CancellationToken()))
                .ReturnsAsync(false);

            // Act
            var handler = new CreateProjectHandler(_methodesRepositoryMock.Object, _projectRepositoryMock.Object, _currentUserMock.Object, _workspaceRepositoryMock.Object);

            // Assert
            await Assert.ThrowsAsync<BusinessRuleException>(
                () => handler.Handle(command, new System.Threading.CancellationToken()));
        }
    }
}
