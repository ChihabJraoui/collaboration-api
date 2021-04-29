using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Persistence.Repositories;

namespace Collaboration.ShareDocs.Persistence
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        private IProjectRepository _projectRepository;
        private IWorkspaceRepository _workspaceRepository;

        public IProjectRepository ProjectRepository
        {
            get
            {

                if (this._projectRepository == null)
                {
                    this._projectRepository = new ProjectRepository(_context);
                }
                return _projectRepository;
            }
        }

        public IWorkspaceRepository WorkspaceRepository
        {
            get
            {

                if (_workspaceRepository == null)
                {
                    this._workspaceRepository = new WorkspaceRepository(_context);
                }
                return _workspaceRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
