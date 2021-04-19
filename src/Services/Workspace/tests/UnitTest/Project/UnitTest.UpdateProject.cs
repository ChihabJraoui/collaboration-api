using Application.Common.Exceptions;
using Application.Project.Commands.UpdateProject;
using Application.Project.Queries.GetProjectById;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.Workspace;
using Xunit;

namespace UnitTest.Project
{
    public class UpdateProjectUnitTests:BaseMock
    {
        [Fact]
        public async void InValidCommand_ShouldThrowException()
        {
            var request = new UpdateProjectCommand
            {
                ProjectId = Guid.Empty,
                Label ="nouvelle label",
                Description = "nouvelle description",
                Icon = "nouvelle icon"
            };

            var project = new Core.Entities.Project{};

            _projectRepositoryMock.Setup(x => x.GetAsync(It.IsAny<GetProjectByIdQuery>()));
            _projectRepositoryMock.Setup(z => z.UpdateAsync(request,project ,_currentUserMock.Object));

            var handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object, _methodesRepositoryMock.Object,_currentUserMock.Object);
           

            await Assert.ThrowsAsync<BusinessRuleException>(
                 () => handler.Handle(request, new System.Threading.CancellationToken()));
        }
        [Fact]
        public async void GetMethodEchec_ShouldThrowBusinessRuleException()
        {
            var request = new UpdateProjectCommand
            {
                ProjectId = Guid.Parse("CE854229-F5E7-4CCE-603B-08D85FD83293"),
                Label = "nouvelle label",
                Description = "nouvelle description",
                Icon = "nouvelle icon"
            };
            var project = new Core.Entities.Project { };

            _projectRepositoryMock.Setup(x => x.GetAsync(It.IsAny<GetProjectByIdQuery>()));
            var handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object, _methodesRepositoryMock.Object, _currentUserMock.Object);


            await Assert.ThrowsAsync<BusinessRuleException>(
                 () => handler.Handle(request, new System.Threading.CancellationToken()));
        }
        [Fact]
        public async void ProjectLabelAlreadyExist_ShouldThrowException()
        {
            //Arrange
            var request = new UpdateProjectCommand
            {
                ProjectId = Guid.Parse("CE854229-F5E7-4CCE-603B-08D85FD83293"),
                Label = "nouvelle label",
                Description = "nouvelle description",
                Icon = "nouvelle icon"
            };
            this._methodesRepositoryMock.Setup(x => x.UniqueName(request.Label, new System.Threading.CancellationToken()))
                .ReturnsAsync(false);

            // Act
            var handler = new UpdateProjectCommandHandler(_projectRepositoryMock.Object, _methodesRepositoryMock.Object, _currentUserMock.Object);

            // Assert
            await Assert.ThrowsAsync<BusinessRuleException>(
                () => handler.Handle(request, new System.Threading.CancellationToken()));
        }
    }
}
