﻿using IdyllicWeb.Infrastructure.Constants;
using IdyllicWeb.Infrastructure.Helpers.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdyllicWeb.Infrastructure.Filters
{
    public class FooterMenuAttribute : ActionFilterAttribute
    {
        public string MenuArea { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var siteMenuManager = new SiteMenuManager();
            filterContext.Controller.ViewBag.FooterMenuItems = siteMenuManager.GetMenuItems(PositionType.Bottom, MenuArea).ToList();
            base.OnActionExecuting(filterContext);
        }
    }
}