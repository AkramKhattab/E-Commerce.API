using AutoMapper;
using Ecommerce.Store.Core.Dtos;
using Ecommerce.Store.Core.Dtos.Products;
using Ecommerce.Store.Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Mapping.Products
{
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {

            CreateMap<Product, ProductDto>()
                .ForMember(d => d.BrandName, options => options.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.TypeName, options => options.MapFrom(s => s.Type.Name))
                
                //.ForMember(d => d.PictureUrl, options => options.MapFrom(s => $"{configuration["BASEURL"]}{s.PictureUrl}"))
                .ForMember(d => d.PictureUrl, options => options.MapFrom(new PictureUrlResolver(configuration)))
                ;

            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();

        }
    }
}
