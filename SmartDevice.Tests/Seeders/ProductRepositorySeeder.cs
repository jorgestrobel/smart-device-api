using Microsoft.EntityFrameworkCore;
using SmartDevice.Data;
using SmartDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDevice.Tests.Seeders
{
    internal class ProductRepositorySeeder
    {
        internal SmartDeviceDbContext Seed()
        {
            var context = new SmartDeviceDbContext(new DbContextOptionsBuilder<SmartDeviceDbContext>().UseInMemoryDatabase(nameof(ProductRepositorySeeder)).Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var cat1 = new Category { Id = 1, Name = "Category1", Codigo = CategoryCodes.Lights };
            var cat2 = new Category { Id = 2, Name = "Category2", Codigo = CategoryCodes.Lights };

            context.Categories.AddRange(cat1, cat2);
            
            context.SaveChanges();
            return context;
        }
    }
}
