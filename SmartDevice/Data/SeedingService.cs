using SmartDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Data
{
    public class SeedingService
    {
        private readonly SmartDeviceDbContext context;

        public SeedingService(SmartDeviceDbContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            context.Database.EnsureCreated();

            //=== Categories ===
            PopulateCategories();
            //=== Users ===
            PopulateUsers();

        }


        private void PopulateUsers()
        {
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
            new User{Login="zebostao", Password = "test123" , Type = "admin", Name = "Unknown"},
            };
            foreach (User user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();
        }
        private void PopulateCategories()
        {
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            var categories = new Category[]
            {
            new Category{Name="Lumières", Codigo = CategoryCodes.Lights},
            new Category{Name="Détecteurs de mouvements", Codigo = CategoryCodes.MotionDetectors},
            new Category{Name="Ecran intelligent", Codigo = CategoryCodes.SmartDisplay},
            new Category{Name="Prise intelligent", Codigo = CategoryCodes.SmartOutlets},
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
        }
    }
}
