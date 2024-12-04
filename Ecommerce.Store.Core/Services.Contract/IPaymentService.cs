using Ecommerce.Store.Core.Dtos.Baskets;
using Ecommerce.Store.Core.Entities;
using Ecommerce.Store.Core.Entities.Order;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Services.Contract
{
    public interface IPaymentService
    {

       Task<CustomerBasketDto> CreateOrUpdatePaymentIntentIdAsync(string basketId);

       Task<Order> UpdatePaymentIntentForSucceedOrFailed(string paymentIntentId, bool flag);

    }
}
