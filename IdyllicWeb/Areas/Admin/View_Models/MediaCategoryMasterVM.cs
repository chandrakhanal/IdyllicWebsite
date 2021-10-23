﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Areas.Admin.View_Models
{
    public class MediaCategoryMasterVM
    {
        [Key]
        [Required(ErrorMessage = "Please Enter MediaCategory Id")]
        [Display(Name = "MediaCategory Id")]
        public int MediaCategoryId { get; set; }

        [Required(ErrorMessage = "Please Enter MediaCategory Name")]
        [Display(Name = "MediaCategory Name")]
        public string MediaCategoryName { get; set; }

        public string UserRole { get; set; }
    }
    //public class MediaCategoryMasterIndxVM : MediaCategoryMasterVM
    //{
    //}
    //public class MediaCategoryMasterCrtVM : MediaCategoryMasterVM
    //{
    //}
    //public class MediaCategoryMasterUpVM : MediaCategoryMasterVM
    //{
    //}
}