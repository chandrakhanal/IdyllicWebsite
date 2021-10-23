using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdyllicWeb.View_Models
{
    public class ContactUSVM
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
    public class ContactUSIndexVM : ContactUSVM
    {

    }
    public class ContactUSCrtVM : ContactUSVM
    {

    }
    public class ContactUSUPVM : ContactUSVM
    {

    }
}