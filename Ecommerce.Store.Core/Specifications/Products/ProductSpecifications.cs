using Ecommerce.Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ecommerce.Store.Core.Specifications.Products
{
    public class ProductSpecifications : BaseSpecification<Product, int>
    {
        public ProductSpecifications(int id) : base(P => P.Id == id)
        {
            ApplyIncludes();
        }
        public ProductSpecifications(ProductSpecParams productSpec) : base(
            P =>
            (string.IsNullOrEmpty(productSpec.Search) || P.ProductName.ToLower().Contains(productSpec.Search))
            &&
            (!productSpec.BrandId.HasValue || productSpec.BrandId == P.BrandId)
            &&
            (!productSpec.TypeId.HasValue || productSpec.TypeId == P.TypeId)
            )
        {
            if (!string.IsNullOrEmpty(productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.ProductName);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.ProductName);
            }
            ApplyIncludes();
            ApplyPagination(productSpec.PageSize * (productSpec.PageIndex - 1), productSpec.PageSize);
        }
        private void ApplyIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Type);
        }
    }
}