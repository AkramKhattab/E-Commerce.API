using Ecommerce.Store.Core.Dtos;
using Ecommerce.Store.Core.Dtos.Products;
using Ecommerce.Store.Core.Helper;
using Ecommerce.Store.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Services.Contract
{
    public interface IProductService
    {
        Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec);
        Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync();
        Task<ProductDto>GetProductById(int id);
    }
}
