using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdyllicWeb.View_Models
{
    public class BlogVM
    {
        public int BlogId { get; set; }
        public string UserId { get; set; }
        public string Blogs { get; set; }
        public string BlogDate { get; set; }
    }
    public class BlogIndexVM:BlogVM
    {

    }
    public class BlogCrtVM : BlogVM
    {

    }
    public class BlogUpVM : BlogVM
    {

    }
}