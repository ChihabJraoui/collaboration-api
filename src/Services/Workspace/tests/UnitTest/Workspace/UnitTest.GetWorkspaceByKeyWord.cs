using Application.Common.Exceptions;
using Application.Workspace.Queries.GetWorkspaceByKeyWord;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest.Workspace
{
    public class GetWorkspaceByKeyWordTests:BaseMock
    {
        [Fact]
        public async void InExisteWorkspace_ShouldThrowException()
        {
            //Arrange
            var query = new GetWorkspaceByKeyWord { KeyWord = "hif" };
            var workspacesDtos = new List<WorkspacesDtoKW>();
            var workspaces = new WorkspaceDtoLists { Workspaces = workspacesDtos };
            _workspaceRepositoryMock.Setup(w => w.GetByKeyWord(It.IsAny<GetWorkspaceByKeyWord>())).ReturnsAsync(workspaces);
            //Act
            var handler = new GetWorkspaceByKeyWordHandler(_workspaceRepositoryMock.Object);
            //Assert
            await Assert.ThrowsAsync<BusinessRuleException>(() => handler.Handle(query, new System.Threading.CancellationToken()));

        }
    }
}
