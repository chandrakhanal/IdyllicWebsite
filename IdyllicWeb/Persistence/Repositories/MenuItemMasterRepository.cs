using IdyllicWeb.Areas.Admin.View_Models;
using IdyllicWeb.Core.IRepositories;
using IdyllicWeb.Data_Contexts;
using IdyllicWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IdyllicWeb.Persistence.Repositories
{
    public class MenuItemMasterRepository : Repository<MenuItemMaster>, IMenuItemMasterRepository
    {
        public MenuItemMasterRepository(DbContext context) : base(context)
        {
        }
        public void UpdateMenuSortOrder(MenuItemMaster menuItem)
        {
            IdyllicWebContext.MenuItemMstr.Attach(menuItem);
            IdyllicWebContext.Entry(menuItem).Property(x => x.SortOrder).IsModified = true;
        }
        public IEnumerable<SelectListItem> GetParentMenus()
        {
            List<SelectListItem> parentMenus = IdyllicWebContext.MenuItemMstr
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = "0",
                Text = "Root"
            };
            parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetParentMenus(string area)
        {
            List<SelectListItem> parentMenus = IdyllicWebContext.MenuItemMstr
                .Where(n => n.MenuArea == area)
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = "0",
                Text = "Root"
            };
            parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }
        public IEnumerable<SelectListItem> GetParentMenus(int parentmenuid)
        {
            List<SelectListItem> parentMenus = IdyllicWebContext.MenuItemMstr
                .Where(n => n.ParentId == parentmenuid)
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            //var ddltip = new SelectListItem()
            //{
            //    Value = "0",
            //    Text = "Root"
            //};
            //parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }

        public IEnumerable<SelectListItem> GetMenuWithParent(string area)
        {
            List<SelectListItem> parentMenus = IdyllicWebContext.MenuItemMstr
                .Where(n => n.MenuArea == area)
                    .OrderByDescending(n => n.SortOrder)
                        .Select(n =>
                        new SelectListItem
                        {
                            Value = n.MenuId.ToString(),
                            Text = n.MenuName
                        }).ToList();
            var ddltip = new SelectListItem()
            {
                Value = "0",
                Text = "Root"
            };
            parentMenus.Insert(0, ddltip);
            return new SelectList(parentMenus, "Value", "Text");
        }

        public async Task<IEnumerable<MenuItemMasterCompleteIndxVM>> GetMenuItemListAsync()
        {
            return await IdyllicWebContext.Database.SqlQuery<MenuItemMasterCompleteIndxVM>("Get_MenuItemMaster").ToListAsync();
        }
        public IdyllicWebContext IdyllicWebContext
        {
            get { return Context as IdyllicWebContext; }
        }
    }
}