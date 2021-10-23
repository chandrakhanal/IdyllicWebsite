using IdyllicWeb.Core;
using IdyllicWeb.Data_Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using IdyllicWeb.Core.IRepositories;
using IdyllicWeb.Persistence.Repositories;
using Microsoft.AspNet.Identity;

namespace IdyllicWeb.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IdyllicWebContext _context;

        #region Interface Instance
        private IMenuItemMasterRepository _MenuItemMstrRepo;
        private IMenuUrlMasterRepository _MenuUrlMstrRepo;
        private IMenuRoleRepository _MenuRoleRepo;
        private IMediaCategoryMasterRepository _MediaCategoryMstrRepo;
        private IMediaGalleryRepository _MediaGalleryRepo;
        private IMediaFileRepository _MediaFileRepo;
        private IUserActivityRepository _UserActivityRepo;

        private ContactUSRepository _ContactUSRepo;
        private BlogRepository _BlogRepo;
        #endregion
        public UnitOfWork(IdyllicWebContext context)
        {
            _context = context;
        }
        #region Interface new Object
        public IMenuItemMasterRepository MenuItemMstrRepo => _MenuItemMstrRepo = _MenuItemMstrRepo ?? new MenuItemMasterRepository(_context);
        public IMenuUrlMasterRepository MenuUrlMstrRepo => _MenuUrlMstrRepo = _MenuUrlMstrRepo ?? new MenuUrlMasterRepository(_context);
        public IMenuRoleRepository MenuRoleRepo => _MenuRoleRepo = _MenuRoleRepo ?? new MenuRoleRepository(_context);
        public IMediaCategoryMasterRepository MediaCategoryMstrRepo => _MediaCategoryMstrRepo = _MediaCategoryMstrRepo ?? new MediaCategoryMasterRepository(_context);
        public IMediaGalleryRepository MediaGalleryRepo => _MediaGalleryRepo = _MediaGalleryRepo ?? new MediaGalleryRepository(_context);
        public IMediaFileRepository MediaFileRepo => _MediaFileRepo = _MediaFileRepo ?? new MediaFileRepository(_context);
        public IUserActivityRepository UserActivityRepo => _UserActivityRepo = _UserActivityRepo ?? new UserActivityRepository(_context);
        public IContactUSRepository ContactUSRepo => _ContactUSRepo = _ContactUSRepo ?? new ContactUSRepository(_context);
        public IBlogRepository BlogRepo => _BlogRepo = _BlogRepo ?? new BlogRepository(_context);

        #endregion

        #region Transaction History
        public int Complete()
        {
            //OnBeforeSaving();
            return _context.SaveChanges();
        }

        public int Complete(Controller controller)
        {
            OnBeforeSaving();
            //string NotifyType = getOperationType();
            int rowAffct = _context.SaveChanges();
            //if (rowAffct > 0) CSPLNotificationExtensions.SetPromptNotification(controller, NotifyType);
            return rowAffct;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Commit()
        {
            OnBeforeSaving();
            _context.SaveChanges();
        }
        public async Task CommitAsync()
        {
            OnBeforeSaving();
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Helper Methods
        private void OnBeforeSaving()
        {
            var entries = _context.ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is Models.BaseEntity)
                {
                    var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["CreatedAt"] = now;
                            entry.CurrentValues["CreatedBy"] = HttpContext.Current.User.Identity.GetUserId();
                            entry.Property("LastUpdatedAt").IsModified = false;
                            entry.Property("LastUpdatedBy").IsModified = false;
                            break;

                        case EntityState.Modified:
                            entry.CurrentValues["LastUpdatedAt"] = now;
                            entry.CurrentValues["LastUpdatedBy"] = HttpContext.Current.User.Identity.GetUserId();
                            entry.Property("CreatedAt").IsModified = false;
                            entry.Property("CreatedBy").IsModified = false;
                            break;
                    }
                }
            }
        }
        #endregion
    }
}