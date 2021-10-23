namespace IdyllicWeb.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using IdyllicWeb.Infrastructure.Constants;
    using IdyllicWeb.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<IdyllicWeb.Data_Contexts.IdyllicWebContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "IdyllicWeb.Data_Contexts.IdyllicWebContext";
        }

        protected override void Seed(IdyllicWeb.Data_Contexts.IdyllicWebContext context)
        {
            context.MenuUrlMstr.AddOrUpdate(x => x.MenuUrlId,
                 new MenuUrlMaster { MenuUrlId = 1, UrlPrefix = "HM", Controller = "Home", Action = "Index", PageType = PageType.Static, UrlArea = "NA", MenuLevel = MenuLevel.NA },
                 new MenuUrlMaster { MenuUrlId = 2, UrlPrefix = "HM", Controller = "Home", Action = "ContactUs", PageType = PageType.Static, UrlArea = "NA", MenuLevel = MenuLevel.NA },
                 new MenuUrlMaster { MenuUrlId = 3, UrlPrefix = "Content1", Controller = "Dynamic", Action = "DynamicPL1", PageType = PageType.Dynamic, UrlArea = "NA", MenuLevel = MenuLevel.Parent },
                 new MenuUrlMaster { MenuUrlId = 4, UrlPrefix = "Content1", Controller = "Dynamic", Action = "DynamicL1", PageType = PageType.Dynamic, UrlArea = "NA", MenuLevel = MenuLevel.Child }
                 );
        }
    }
}
