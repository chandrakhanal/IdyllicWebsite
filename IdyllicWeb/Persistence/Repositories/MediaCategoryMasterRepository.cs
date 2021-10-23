using IdyllicWeb.Core.IRepositories;
using IdyllicWeb.Data_Contexts;
using IdyllicWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdyllicWeb.Persistence.Repositories
{
    public class MediaCategoryMasterRepository : Repository<MediaCategoryMaster>, IMediaCategoryMasterRepository
    {
        public MediaCategoryMasterRepository(DbContext context) : base(context)
        {
        }
        public IEnumerable<SelectListItem> GetMediaCategories()
        {
            List<SelectListItem> mediaCategories = IdyllicWebContext.MediaCategoryMstr
                .OrderBy(n => n.MediaCategoryName)
                .Select(n =>
                new SelectListItem
                {
                    Value = n.MediaCategoryId.ToString(),
                    Text = n.MediaCategoryName
                }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = null,
                Text = "-- Select --"
            };
            mediaCategories.Insert(0, ddltip);
            return new SelectList(mediaCategories, "Value", "Text");
        }
        public IdyllicWebContext IdyllicWebContext
        {
            get { return Context as IdyllicWebContext; }
        }
    }
}