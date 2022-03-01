using Microsoft.EntityFrameworkCore;
using SmartDevice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartDevice.Repositories.IRepository;

namespace SmartDevice.Repositories.Impl
{
    public abstract class GenericRepository<TEntity, TContext> :
        IRepository<TEntity>
        where TEntity : class
        where TContext : SmartDeviceDbContext
    {

        private readonly TContext context;
        protected TContext Context { get { return context; } }

        protected GenericRepository(TContext context)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));
            this.context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await context.AddAsync(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity)
        {
            context.Remove<TEntity>(entity);
            return entity;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            var entity = await context.FindAsync<TEntity>(id);
            if (entity != null)
                context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            context.Update<TEntity>(entity);
            return await Task.FromResult(entity);
        }

        public List<TEntity> GetAll()
        {
            return context.Set<TEntity>().AsNoTracking().ToList();
        }

        public TEntity Add(TEntity entity)
        {
            context.Add(entity);
            return entity;
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}
