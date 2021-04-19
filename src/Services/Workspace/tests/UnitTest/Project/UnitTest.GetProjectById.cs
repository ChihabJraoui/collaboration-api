using Application.Common.Exceptions;
using Application.Project.Queries.GetProjectById;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.Workspace;
using Xunit;

namespace UnitTest.Project
{
    public class GetProjectByIdTest : BaseMock
    {
        [Fact]
        public async void ValideQueryId_ShouldReturnProjectEntity()
        {
            //Arrange
            var query = new GetProjectByIdQuery {
                Id = Guid.Parse("D4360B4F-5C68-4D72-C415-08D85FB2328E"),
            };
            var projectEntity = new Core.Entities.Project { 
                ProjectId = query.Id
            };

            _projectRepositoryMock.Setup(x => x.GetAsync(query)).ReturnsAsync(projectEntity);
            //Act
            var handler = new GetProjectByIdQueryHandler(_projectRepositoryMock.Object);
            var project = await handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            Assert.Equal(projectEntity, project);
             

        }
        [Fact]
        public async void EmptyQuery_ShoudThrowExceptionBusinessRules()
        {
            var query = new GetProjectByIdQuery
            {
                Id = Guid.Empty,
            };
            var projectEntity = new Core.Entities.Project
            {
                ProjectId = query.Id
            };
            _projectRepositoryMock.Setup(x => x.GetAsync(query)).ReturnsAsync(projectEntity);
            //Act
            var handler = new GetProjectByIdQueryHandler(_projectRepositoryMock.Object);
            var project =  handler.Handle(query, new System.Threading.CancellationToken());

            //Assert
            await Assert.ThrowsAsync<BusinessRuleException>(()=> project);
        }
    }
}
