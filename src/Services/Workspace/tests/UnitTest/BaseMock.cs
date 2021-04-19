using Application.Common.Interfaces;
using Application.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace UnitTest.Workspace
{
    public class BaseMock
    {
        protected readonly Mock<IProjectRepository> _projectRepositoryMock;
        protected readonly Mock<IMethodesRepository> _methodesRepositoryMock;
        protected readonly Mock<IWorkspaceRepository> _workspaceRepositoryMock;
        protected readonly Mock<ICurrentUserService> _currentUserMock;
        protected readonly Mock<IFolderRepository> _folderRepositoryMock;
         public BaseMock()
        {
            this._currentUserMock = new Mock<ICurrentUserService>();
            this._workspaceRepositoryMock = new Mock<IWorkspaceRepository>();
            this._projectRepositoryMock = new Mock<IProjectRepository>();
            this._methodesRepositoryMock = new Mock<IMethodesRepository>();
            this._folderRepositoryMock = new Mock<IFolderRepository>();
        }
    }
}
