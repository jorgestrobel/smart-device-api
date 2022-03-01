using SmartDevice.Data;
using SmartDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Repositories.Impl
{
    public class ProductRepository : GenericRepository<Product, SmartDeviceDbContext>, IProductRepository
    {
        public ProductRepository(SmartDeviceDbContext context) : base(context)
        {
        }

        public async Task InsertListOfDevices(List<string> devices, List<Category> categories)
        {
            int index = 0;
            foreach (string device in devices)
            {
                if (index > 0)
                {
                    try
                    {
                        string[] deviceParts = device.Split('|');
                        Product prod = new Product
                        {
                            Name = deviceParts[0],
                            Description = deviceParts[1],
                            Price = Double.Parse(deviceParts[2]),
                            CategoryId = GetCategoryId(int.Parse(deviceParts[3]), categories)
                        };
                        await AddAsync(prod);
                        await SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        //TODO: Save in a file or database all the devices that Insert process failed.
                    }
                }
                index++;
            }
        }

        private int GetCategoryId(int categoryCode, List<Category> categories)
        {
            foreach (Category cat in categories)
            {
                if (categoryCode == (int)cat.Codigo)
                {
                    return cat.Id;
                }
            }
            return 0;
        }
    }
}
