using IdyllicWeb.Core.IRepositories;
using IdyllicWeb.Data_Contexts;
using IdyllicWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Persistence.Repositories
{
    public class MenuRoleRepository : Repository<MenuRole>, IMenuRoleRepository
    {
        public MenuRoleRepository(DbContext context) : base(context)
        {
        }

        public IdyllicWebContext IdyllicWebContext
        {
            get { return Context as IdyllicWebContext; }
        }
    }
}