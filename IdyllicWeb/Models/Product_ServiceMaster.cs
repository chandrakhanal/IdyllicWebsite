using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdyllicWeb.Models
{
    public class Product_ServiceMaster
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string AccUnit { get; set; }
        public string ProductCode { get; set; }
        public string HSNSACCode { get; set; }
    }
}