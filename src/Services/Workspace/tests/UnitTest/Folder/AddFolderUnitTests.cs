using Application.Common.Exceptions;
using Application.Folder.Commands;
using Application.Project.Queries.GetProjectById;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.Workspace;
using Xunit;

namespace UnitTest.Folder
{
    public class AddFolderUnitTests:BaseMock
    {
        [Fact]
        public async void RequestAlreadyExiste_ShouldThrowException()
        {
            var request = new AddFolderCommand {
                FolderParentId = Guid.Parse("CDAFFC6E-2D53-4B5E-1B91-08D86924B8E7"),
                Name = "Folder",
                ProjectId = Guid.Parse("CE854229-F5E7-4CCE-603B-08D85FD83293")
            };
            _folderRepositoryMock.Setup(x => x.UniqueName(request.Name)).ReturnsAsync(false);
            //Act
            var handler = new AddFolderHandler(_folderRepositoryMock.Object,_currentUserMock.Object,_projectRepositoryMock.Object);
            //Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => handler.Handle(request, new System.Threading.CancellationToken()));
        }

        [Fact]
        public async void InexisteProject_ShouldThrowException()
        {
            var request = new AddFolderCommand
            {
                
                FolderParentId = Guid.Parse("CDAFFC6E-2D53-4B5E-1B91-08D86924B8E7"),
                Name = "Folder",
                ProjectId = Guid.Parse("CE854229-F5E7-4CCE-603B-08D85FD83293")
            };
            var project = new Core.Entities.Project { };
            
            _projectRepositoryMock.Setup(x => x.GetAsync(It.IsAny<GetProjectByIdQuery>()))
                .ReturnsAsync(project);
            //Act
            var handler = new AddFolderHandler(_folderRepositoryMock.Object, _currentUserMock.Object, _projectRepositoryMock.Object);
            //Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => handler.Handle(request, new System.Threading.CancellationToken()));

        }
        
    }
}
