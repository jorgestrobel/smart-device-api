using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Repositories
{
    public interface IRepository
    {
        public interface IRepository<T> where T : class
        {
            Task<List<T>> GetAllAsync();

            List<T> GetAll();

            Task<T> GetAsync(int id);

            Task<T> AddAsync(T entity);

            T Add(T entity);

            Task<T> UpdateAsync(T entity);

            T Delete(T entity);

            Task<int> SaveChangesAsync();

            int SaveChanges();
        }
    }
}
