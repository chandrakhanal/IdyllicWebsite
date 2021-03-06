using System.Web.Mvc;

namespace IdyllicWeb.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "Admin",
               "Admin/{controller}/{action}/{id}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new[] { "IdyllicWeb.Areas.Admin.Controllers" }
           );
        }
    }
}