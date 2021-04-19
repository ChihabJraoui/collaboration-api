using Application.Workspace.Queries.GetAllWorkspaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest.Workspace
{
    public class GetWorkspacesTest : BaseMock
    {
        [Fact]

        public async void AValidQuery_ShouldGiveAllWorkspacest()
        {
            //Arrage
            var request = new GetAllWorkspaceQuery { };

            var workspace1 = new WorkspaceDto { Name="hi"};
            var workspace2 = new WorkspaceDto { Name = "hij" };
            var workspace3 = new WorkspaceDto { Name = "hijs" };
            var workspace4 = new WorkspaceDto { Name = "his" };
            var workspace5 = new WorkspaceDto { Name = "hiq" };
            var workspaceDtos = new List<WorkspaceDto>();
            workspaceDtos.Add(workspace1);
            workspaceDtos.Add(workspace2);
            workspaceDtos.Add(workspace3);
            workspaceDtos.Add(workspace4);
            workspaceDtos.Add(workspace5);


            var Listes = new WorkspacesDTOLists
            {
                WorkspacesLists= workspaceDtos
            };


            this._workspaceRepositoryMock.Setup(x => x.GetAllAsync(request, new System.Threading.CancellationToken()))
                .ReturnsAsync(Listes);
                

            //Act
            var handler = new GetAllWorkspacesQueryHandler(_workspaceRepositoryMock.Object);
            var workspaceList = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(workspaceList);

        }
    
    }
}
