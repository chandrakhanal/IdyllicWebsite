using IdyllicWeb.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using IdyllicWeb.Core.IRepositories;

namespace IdyllicWeb.Core
{
    public interface IUnitOfWork : IDisposable
    {
        #region admin
       
        IMediaCategoryMasterRepository MediaCategoryMstrRepo { get; }
        IMediaGalleryRepository MediaGalleryRepo { get; }
        IMediaFileRepository MediaFileRepo { get; }
       
        #endregion

       
        int Complete();
        int Complete(Controller controller);
        void Commit();
        Task CommitAsync();
    }
}
