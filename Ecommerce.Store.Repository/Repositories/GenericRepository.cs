using Ecommerce.Store.Core.Entities;
using Ecommerce.Store.Core.Repositories.Contract;
using Ecommerce.Store.Core.Specifications;
using Ecommerce.Store.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Repository.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly EcommerceDbContext _context;


        public GenericRepository(EcommerceDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return (IEnumerable<TEntity>) await _context.Products.OrderBy(P => P.ProductName).Include(P => P.Brand).Include(P => P.Type).ToListAsync();
            }
            return await _context.Set<TEntity>().ToListAsync();
        }
        //

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }
        //

        public async Task AddAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
        }
        //

        public void Update(TEntity entity)
        {
            _context.Update(entity);
        }
        //

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }
        //

        public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
           return await ApplySpecifications(spec).ToListAsync();
        }
        //

        public  async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecifications(spec).FirstOrDefaultAsync();
        }
        //

        public Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec)
        {
            return ApplySpecifications(spec).CountAsync();
        }
        //

        private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), spec);
        }
        //

    }
}
