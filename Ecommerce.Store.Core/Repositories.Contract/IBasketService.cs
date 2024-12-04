using Ecommerce.Store.Core.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Repositories.Contract
{
    public interface IBasketService
    {
        Task<CustomerBasketDto?> GetBasketAsync(string basketId);
        Task<CustomerBasketDto?> UpdateBasketAsync(CustomerBasketDto basketDto);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}