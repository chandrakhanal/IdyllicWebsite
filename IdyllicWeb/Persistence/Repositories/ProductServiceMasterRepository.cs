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
using IdyllicWeb.Areas.Admin.View_Models;


namespace IdyllicWeb.Persistence.Repositories
{
    public class ProductServiceMasterRepository : Repository<Product_ServiceMaster>, IProductServiceMasterRepository
    {
        public ProductServiceMasterRepository(DbContext context) : base(context)
        {
        }
    }
}