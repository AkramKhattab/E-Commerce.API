using AutoMapper;
using AutoMapper.Execution;
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
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BASEURL"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
