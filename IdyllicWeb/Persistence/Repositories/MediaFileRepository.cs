using IdyllicWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using IdyllicWeb.Core.IRepositories;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Persistence.Repositories
{
    public class MediaFileRepository : Repository<MediaFile>, IMediaFileRepository
    {
        public MediaFileRepository(DbContext context) : base(context)
        {
        }
    }
}