using IdyllicWeb.Core.IRepositories;
using IdyllicWeb.Data_Contexts;
using IdyllicWeb.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdyllicWeb.Persistence.Repositories
{
    public class UserActivityRepository: Repository<UserActivity>, IUserActivityRepository
    {
        public UserActivityRepository(DbContext context) : base(context)
        {
        }
    }
}