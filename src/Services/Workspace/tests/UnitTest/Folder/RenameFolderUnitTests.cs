using Application.Common.Exceptions;
using Application.Folder.Commands.RenameFolder;
using Application.Folder.Commands.UpdateFolder;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.Workspace;
using Xunit;

namespace UnitTest.Folder
{
    public class RenameFolderUnitTests : BaseMock
    {
        [Fact]
        public async void RequestAlreadyExiste_ShouldThrowException()
        {
            var request = new RenameFolderCommand
            {
                Name = "Folder",
                FolderId = Guid.Parse("C2771484-2E5A-4E13-F07E-08D8691A4AAE")
            };
            _folderRepositoryMock.Setup(x => x.UniqueName(request.Name)).ReturnsAsync(false);
            //Act
            var handler = new RenameFolderHandler(_folderRepositoryMock.Object, _currentUserMock.Object);
            //Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => handler.Handle(request, new System.Threading.CancellationToken()));
        }


        [Fact]
        public async void InexisteProject_ShouldThrowException()
        {
            var request = new RenameFolderCommand
            {

                FolderId = Guid.Parse("CDAFFC6E-2D53-4B5E-1B91-08D86924B8E7"),
                Name = "Folder2",
            };
            var folder = new Core.Entities.Folder("folder1") { };

            _folderRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(folder);
            //Act
            var handler = new RenameFolderHandler(_folderRepositoryMock.Object, _currentUserMock.Object);
            //Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => handler.Handle(request, new System.Threading.CancellationToken()));

        }
    }

}