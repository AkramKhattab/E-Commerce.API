using Ecommerce.Store.Core;
using Ecommerce.Store.Core.Entities;
using Ecommerce.Store.Core.Repositories.Contract;
using Ecommerce.Store.Repository.Data.Contexts;
using Ecommerce.Store.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceDbContext _context;
        private Hashtable _repositories;
        public UnitOfWork(EcommerceDbContext context)
        {
            _context = context;
            _repositories = new Hashtable();
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
           var type = typeof(TEntity).Name ;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity, TKey>(_context);
                _repositories.Add(type, repository);
            }
            return _repositories[type] as IGenericRepository<TEntity, TKey>;
        }
    }
}
