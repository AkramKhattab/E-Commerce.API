using Ecommerce.Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Specifications
{
    public interface ISpecifications<TEntity, TKey>  where TEntity : BaseEntity<TKey>
    {
        public Expression<Func<TEntity,bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }
        public Expression<Func<TEntity, object>> OrderBy { get; set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; set; }

        public int Skip {  get; set; }
        public int Take { get; set; }
        public bool IsPaginationEnabled { get; set; }

    }

    // _context.Products.Include(P => P.Brand).Include(P => P.Type).FirstOrDefaultAsync(P => P.Id == id as int?) as TEntity;
}