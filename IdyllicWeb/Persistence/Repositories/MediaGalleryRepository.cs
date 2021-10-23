using IdyllicWeb.Core.IRepositories;
using IdyllicWeb.Data_Contexts;
using IdyllicWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Persistence.Repositories
{
    public class MediaGalleryRepository : Repository<MediaGallery>, IMediaGalleryRepository
    {
        public MediaGalleryRepository(DbContext context) : base(context)
        {
        }
        public void UpdateRecord(MediaGallery objmediaGallery)
        {
            IdyllicWebContext.MediaGalleries.Attach(objmediaGallery);
            IdyllicWebContext.Entry(objmediaGallery).State = EntityState.Modified;
            if (objmediaGallery.iMediaFiles.Count >= 1)
            {
                var removeOldItem = IdyllicWebContext.MediaFiles.Where(x => x.MediaGalleryId == objmediaGallery.MediaGalleryId).ToList();
                if (removeOldItem != null)
                {
                    IdyllicWebContext.MediaFiles.RemoveRange(removeOldItem);
                    //IdyllicWebContext.SaveChanges();
                }
                foreach (var up in objmediaGallery.iMediaFiles)
                {
                    IdyllicWebContext.MediaFiles.Attach(up);
                    IdyllicWebContext.Entry(up).State = EntityState.Added;
                    //IdyllicWebContext.Entry(up).Property(x => x.IsAttend).IsModified = true;
                }
            }
        }

        public IdyllicWebContext IdyllicWebContext
        {
            get { return Context as IdyllicWebContext; }
        }

    }
}