using SmartDevice.Models;
using SmartDevice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Services.Products.Impl
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public void InsertListOfDevices(List<string> devices)
        {
            List<Category> categories = categoryRepository.GetAll();
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
                        productRepository.Add(prod);
                        productRepository.SaveChanges();
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
