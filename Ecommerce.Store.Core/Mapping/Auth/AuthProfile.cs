using AutoMapper;
using Ecommerce.Store.Core.Dtos.Auth;
using Ecommerce.Store.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Mapping.Auth
{
    public class AuthProfile : Profile
    {
        public AuthProfile() 
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
