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
    internal class UserRepositorySeeder
    {
        internal SmartDeviceDbContext Seed()
        {
            var context = new SmartDeviceDbContext(new DbContextOptionsBuilder<SmartDeviceDbContext>().UseInMemoryDatabase(nameof(UserRepositorySeeder)).Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var user1 = new User { Id = 1, Name = "Name1", Login = "zebostao" , Password = "12345", Type = "admin"};

            context.Users.Add(user1);
            
            context.SaveChanges();
            return context;
        }
    }
}
