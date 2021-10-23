using System.Web;
using System.Web.Mvc;

namespace IdyllicWeb.Infrastructure.Filters
{
    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["userId"] == null)
            {
                filterContext.Result = new RedirectResult("~/admin/account/login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}