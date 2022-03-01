using SmartDevice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartDevice.Repositories.IRepository;

namespace SmartDevice.Repositories
{
    public interface IUserRepository: IRepository<User>
    {
        Task<User> FindByLogin(string login);
    }
}
