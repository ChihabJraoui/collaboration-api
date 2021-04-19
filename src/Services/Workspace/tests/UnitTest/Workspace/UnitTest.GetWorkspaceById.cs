using Application.Common.Exceptions;
using Application.Workspace.Queries.GetWorkspace;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentValidation;

namespace UnitTest.Workspace
{
    public class GetWorkspaceByIdTests: BaseMock
    {
        [Fact]
        public async void AnvalidGuid_ShouldNotReturnGuidEmpty()
        {
            //Arrage
            var request = new GetWorkspaceByIdQuery
            {
                WorkspaceRequestId =  Guid.Parse("C8CBEC70-4F55-4B20-B2EB-08D854A16944"),
            };
            var workspace = new Core.Entities.Workspace{ 
                Id= request.WorkspaceRequestId
            } ;
            this._workspaceRepositoryMock.Setup(x => x.GetAsync(request))
                .ReturnsAsync(workspace);

            //Act
            var handler = new GetWorkspaceByIdHandler(_workspaceRepositoryMock.Object);
            var result = await handler.Handle(request, new System.Threading.CancellationToken());
            
            Assert.NotEqual(result.Id, Guid.Empty);
        }
    }
}
