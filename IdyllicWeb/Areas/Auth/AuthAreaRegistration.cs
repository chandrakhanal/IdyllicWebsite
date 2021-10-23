using System.Web.Mvc;

namespace IdyllicWeb.Areas.Auth
{
    public class AuthAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Auth";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
               "Auth",
               "Auth/{controller}/{action}/{id}",
               new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               new[] { "IdyllicWeb.Areas.Auth.Controllers" }
           );
        }
    }
}