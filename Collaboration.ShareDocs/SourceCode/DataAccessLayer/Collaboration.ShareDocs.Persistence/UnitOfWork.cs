using Collaboration.ShareDocs.Persistence.Entities;
using Collaboration.ShareDocs.Persistence.Hubs;
using Collaboration.ShareDocs.Persistence.Interfaces;
using Collaboration.ShareDocs.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Collaboration.ShareDocs.Persistence
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {

        private readonly AppDbContext _context;
        private readonly IHubContext<NotificationHub,IHubClient> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UnitOfWork(AppDbContext appDbContext, IHubContext<NotificationHub,IHubClient> hubContext,UserManager<ApplicationUser> userManager)
        {
            _context = appDbContext;
            _hubContext = hubContext;
            _userManager = userManager;
        }

        private IFolderRepository _folderRepository;
        public IFolderRepository FolderRepository
        {
            get
            {
                if (_folderRepository == null)
                {
                    this._folderRepository = new FolderRepository(_context);
                }
                return _folderRepository;
            }
        }

        private IFileRepository _fileRepository;
        public IFileRepository FileRepository
        {
            get
            {

                if (this._fileRepository == null)
                {
                    this._fileRepository = new FileRepository(_context);
                }
                return _fileRepository;
            }
        }

        private IProjectRepository _projectRepository;
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

        private IWorkspaceRepository _workspaceRepository;
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

        private IMethodesRepository _methodRepository;
        public IMethodesRepository MethodRepository
        {
            get
            {

                if (_methodRepository == null)
                {
                    this._methodRepository = new MethodesRepository(_context);
                }
                return _methodRepository;
            }
        }

        private IFollowRepository _followRepository;
        public IFollowRepository FollowRepository
        {
            get
            {

                if (_followRepository == null)
                {
                    this._followRepository = new FollowRepository(_context);
                }
                return _followRepository;
            }
        }
        private INotificationRepository _notificationRepository;
       

        public INotificationRepository NotificationRepository
        {
            get
            {
                if(_notificationRepository == null)
                {
                    this._notificationRepository = new NotificationRepository(_context,_hubContext, _userManager);
                }
                return _notificationRepository;
            }
        }
        private INotificationApplicationUser _userNotificationRepository;
        public INotificationApplicationUser UserNotificationRepository
        {
            get
            {
                if (_userNotificationRepository == null)
                {
                    this._userNotificationRepository = new UserNotificationRepository(_context, _hubContext, _userManager);
                }
                return _userNotificationRepository;
            }
        }
        private IUserProjectRepository _userProjectRepository;
        public IUserProjectRepository UserProjectRepository
        {
            get
            {
                if (_userProjectRepository == null)
                {
                    this._userProjectRepository = new UserProjectRepository(_context);
                }
                return _userProjectRepository;
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
