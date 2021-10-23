using IdyllicWeb.Areas.Admin.Models;
using IdyllicWeb.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace IdyllicWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = CustomRoles.Admin)]
    [CSPLHeaders]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {

            return View();
        }
        //public static string GetFullName(this System.Security.Principal.IPrincipal usr)
        //{
        //    var fullNameClaim = ((ClaimsIdentity)usr.Identity).FindFirst("FName");
        //    if (fullNameClaim != null)
        //        return fullNameClaim.Value;

        //    return "";
        //}
    }
}