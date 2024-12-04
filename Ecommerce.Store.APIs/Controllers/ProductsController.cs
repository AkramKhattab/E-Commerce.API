using Ecommerce.Store.APIs.Attributes;
using Ecommerce.Store.APIs.Errors;
using Ecommerce.Store.Core.Dtos;
using Ecommerce.Store.Core.Dtos.Products;
using Ecommerce.Store.Core.Helper;
using Ecommerce.Store.Core.Services.Contract;
using Ecommerce.Store.Core.Specifications.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Ecommerce.Store.APIs.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet] // Get BaseUrl/api/Products
        [Cached(100)]
        // sort : Name | Price = Accending/Decending
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec ) // endpoint
        {
            var result = await _productService.GetAllProductsAsync(productSpec);

            return Ok(result); //200
        }
        //

        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")] // Get BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes() //endpoint
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result); // 200
        }
        //

        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")] // Get BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands() //endpoint
        {
           var result = await _productService.GetAllBrandsAsync();
            
            return Ok(result); // 200
        }
        //

        [Authorize]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // BaseUrl/api/Products
        public async Task<ActionResult<ProductDto>> GetProductById(int? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse (400));        
            var result = await _productService.GetProductById(id.Value);

            if (result is null) return NotFound(new ApiErrorResponse(404, $"The Product With id : {id} Not Found !"));
            
            return Ok(result);
        
        }
        //


    }
}
