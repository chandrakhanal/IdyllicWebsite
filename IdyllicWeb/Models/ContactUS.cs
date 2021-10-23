using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdyllicWeb.Models
{
    public class ContactUS
    {
        [Key]
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}