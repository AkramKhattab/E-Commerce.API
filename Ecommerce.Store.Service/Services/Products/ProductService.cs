using AutoMapper;
using Ecommerce.Store.Core;
using Ecommerce.Store.Core.Dtos;
using Ecommerce.Store.Core.Dtos.Products;
using Ecommerce.Store.Core.Entities;
using Ecommerce.Store.Core.Helper;
using Ecommerce.Store.Core.Services.Contract;
using Ecommerce.Store.Core.Specifications;
using Ecommerce.Store.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //
        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductSpecifications(productSpec);

            var products = await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec);
            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);

            var countSpec = new ProductWithCountSpecifications(productSpec);

            var count = await _unitOfWork.Repository<Product, int>().GetCountAsync(countSpec);

            return new PaginationResponse<ProductDto>(productSpec.PageSize, productSpec.PageIndex, count, mappedProducts);
        }
        //

        public async Task<ProductDto> GetProductById(int id)
        {
            var spec = new ProductSpecifications(id);
            return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spec));
        }
        //

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());
        }
        //

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
            var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
            var mappedTypes = _mapper.Map<IEnumerable<TypeBrandDto>>(types);
            return mappedTypes;
        }
        //


    }
}
