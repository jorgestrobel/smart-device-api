using SmartDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Data
{
    public class DbInitializer
    {
        public static void Initialize(SmartDeviceDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
            new Category{Id=1, Name="Lumières"},
            new Category{Id=2, Name="Détecteurs de mouvements"},
            new Category{Id=3, Name="Ecran intelligent"},
            new Category{Id=4, Name="Prise intelligent"},
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
        }
    }
}
