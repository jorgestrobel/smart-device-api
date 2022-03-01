using SmartDevice.Data;
using SmartDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Repositories.Impl
{
    public class CategoryRepository : GenericRepository<Category, SmartDeviceDbContext>, ICategoryRepository
    {
        public CategoryRepository(SmartDeviceDbContext context) : base(context)
        {
        }
    }
}
