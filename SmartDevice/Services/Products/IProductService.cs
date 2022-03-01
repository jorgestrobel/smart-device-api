using SmartDevice.Models;
using SmartDevice.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Services.Products
{
    public interface IProductService
    {
        void InsertListOfDevices(List<string> devices);
    }
}
