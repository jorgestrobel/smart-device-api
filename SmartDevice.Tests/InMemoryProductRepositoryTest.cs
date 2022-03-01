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
    public class InMemoryProductRepositoryTest
    {
        [Fact]
        public async void Can_Insert_All_Devices_From_File()
        {
            using (var context = new ProductRepositorySeeder().Seed())
            { 
            
                CategoryRepository categoryRepository = new CategoryRepository(context);
                List<Category> categories = await categoryRepository.GetAllAsync();
                ProductRepository productRepository = new ProductRepository(context);

                List<string> listDevices = File.ReadAllLines(GetCsvPath("devices.csv")).ToList();
                Assert.True(listDevices.Count == 8); //including the header line
                await productRepository.InsertListOfDevices(listDevices, categories);

                List<Product> products = await productRepository.GetAllAsync();
                Assert.True(products.Count == 7); //count without the header line
            }
        }

        [Fact]
        public async void Can_Insert_All_Devices_In_Correct_Format_From_File()
        {
            using (var context = new ProductRepositorySeeder().Seed())
            { 
                CategoryRepository categoryRepository = new CategoryRepository(context);
                List<Category> categories = await categoryRepository.GetAllAsync();
                ProductRepository productRepository = new ProductRepository(context);

                List<string> listDevices = File.ReadAllLines(GetCsvPath("devices-with-wrong-price.csv")).ToList();
                //including the header line and two lines with incorrect prices
                Assert.True(listDevices.Count == 8);
                await productRepository.InsertListOfDevices(listDevices, categories);

                List<Product> products = await productRepository.GetAllAsync();
                Assert.True(products.Count == 5); //count of all devices in correct format
            }
        }

        private static string GetCsvPath(string fileName)
        {
            string assemblyDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string projectDir = assemblyDirectory.Substring(0, assemblyDirectory.IndexOf("bin"));
            return Path.Combine(projectDir, "TestFiles", fileName);
        }

    }
}
