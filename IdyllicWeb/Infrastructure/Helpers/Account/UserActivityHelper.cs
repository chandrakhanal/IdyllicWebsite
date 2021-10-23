using IdyllicWeb.Data_Contexts;
using IdyllicWeb.Models;
using IdyllicWeb.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Infrastructure.Helpers.Account
{
    public static class UserActivityHelper
    {
        public static void SaveUserActivity(string data, string url)
        {
            
            string userName = HttpContext.Current.User.Identity.Name;
            string ipAddress = HttpContext.Current.Request.UserHostAddress;
            //string controllerName = HttpContext.Current.RouteData.Values["controller"].ToString();
            //string actionName = HttpContext.Current.RouteData.Values["action"].ToString();
            using (var uow = new UnitOfWork(new IdyllicWebContext()))
            {
                var userActivity = new UserActivity
                {
                    Data = data,
                    Url = url,
                    UserName = userName,
                    IpAddress = ipAddress,
                    ActivityDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"))
            };
                uow.UserActivityRepo.Add(userActivity);
                uow.Commit();
            }
        }
    }
}