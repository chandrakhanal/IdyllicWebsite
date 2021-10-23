using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Infrastructure.Constants
{
    public class secConst
    {
        private static string csalt; // field
        public static string cSalt   // property
        {
            get { return csalt; }
            set { csalt = value; }
        }
    }
}