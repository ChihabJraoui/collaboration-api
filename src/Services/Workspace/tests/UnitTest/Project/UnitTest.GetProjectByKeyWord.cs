using Application.Common.Exceptions;
using Application.Project.Queries.GetProjectByKeyWord;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using UnitTest.Workspace;
using Xunit;

namespace UnitTest.Project
{
    public class GetProjectByKeyWordUnitTests:BaseMock
    {
        [Fact]
        public async void InExisteProject_ShouldThrowException()
        {
            var query = new GetProjectByKeyWordQuery { KeyWord = "hif" };
            var projectsDtos = new List<ProjectDto>();
            var projects = new ProjectsDTOLists { Projects= projectsDtos };
            _projectRepositoryMock.Setup(w => w.GetByKeyWordAsync(It.IsAny<GetProjectByKeyWordQuery>())).ReturnsAsync(projects);

            var handler = new GetProjectByKeyWordHandler(_projectRepositoryMock.Object);

            await Assert.ThrowsAsync<BusinessRuleException>(() => handler.Handle(query, new System.Threading.CancellationToken()));

        }
    }
}
