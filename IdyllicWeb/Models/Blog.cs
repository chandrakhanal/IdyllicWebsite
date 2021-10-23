using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdyllicWeb.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public string UserId { get; set; }
        public string Blogs { get; set; }
        public string BlogDate { get; set; }
    }
}