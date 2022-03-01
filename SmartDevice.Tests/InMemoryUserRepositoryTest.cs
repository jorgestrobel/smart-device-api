using SmartDevice.Models;
using SmartDevice.Repositories.Impl;
using SmartDevice.Tests.Seeders;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using System.Linq;

namespace SmartDevice.Tests
{
    public class InMemoryUserRepositoryTest
    {
        [Fact]
        public async void Can_Find_User_By_Login()
        {

            using (var context = new UserRepositorySeeder().Seed())
            { 
            
                UserRepository repository = new UserRepository(context);
                User user = await repository.FindByLogin("zebostao");
                Assert.NotNull(user);
                Assert.True(user.Login.Equals("zebostao"));
            }
        }
    }
}
