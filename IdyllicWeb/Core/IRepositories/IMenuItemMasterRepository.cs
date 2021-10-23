using IdyllicWeb.Areas.Admin.View_Models;
using IdyllicWeb.Models;
using IdyllicWeb.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IdyllicWeb.Core.IRepositories
{
    public interface IMenuItemMasterRepository : IRepository<MenuItemMaster>
    {
        Task<IEnumerable<MenuItemMasterCompleteIndxVM>> GetMenuItemListAsync();
        IEnumerable<SelectListItem> GetParentMenus();
        IEnumerable<SelectListItem> GetParentMenus(string area);
        IEnumerable<SelectListItem> GetParentMenus(int parentmenuid);
        void UpdateMenuSortOrder(MenuItemMaster menuItem);
    }
}
