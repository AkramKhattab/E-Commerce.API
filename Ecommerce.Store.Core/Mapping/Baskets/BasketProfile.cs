using AutoMapper;
using Ecommerce.Store.Core.Dtos.Baskets;
using Ecommerce.Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Mapping.Baskets
{
    public class BasketProfile : Profile
    {
        public BasketProfile() 
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        }


    }
}
