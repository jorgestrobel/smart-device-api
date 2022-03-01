using SmartDevice.Data;
using SmartDevice.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartDevice.Repositories.Impl
{
    public class UserRepository : GenericRepository<User, SmartDeviceDbContext>, IUserRepository
    {
        public UserRepository(SmartDeviceDbContext context) : base(context)
        {
        }

        public async Task<User> FindByLogin(string login)
        {
            return await Context.Users.Where(u => u.Login.Equals(login)).FirstAsync();
        }
    }
}
