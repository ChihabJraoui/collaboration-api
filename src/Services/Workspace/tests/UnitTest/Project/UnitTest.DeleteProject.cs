using Application.Common.Exceptions;
using Application.Project.Commands.DeleteProject;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.Workspace;
using Xunit;

namespace UnitTest.Project
{
    public class DeleteProjectTests:BaseMock
    {
        [Fact]
        public async void EmptyGuidProject_ShouldThrowAnException()
        {
            var command = new DeleteProjectCommand
            {
                ProjectId = Guid.Empty,
            };

            _projectRepositoryMock.Setup(x => x.DeleteAsync(command, _currentUserMock.Object));
            var handler =  new DeleteProjectHandler(_projectRepositoryMock.Object, _currentUserMock.Object);
            //Assert
            await Assert.ThrowsAsync<BusinessRuleException>(
                 () => handler.Handle(command, new System.Threading.CancellationToken()));
        }
    }
}
