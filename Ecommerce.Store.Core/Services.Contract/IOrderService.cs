using Ecommerce.Store.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Services.Contract
{
    public interface IOrderService
    {
        Task<Order> CreatOrderAsync(string buyerEmail, string basketId, int deliveryMethodId, Address shippingAddress);
        Task<Order?> GetOrderByIdForSpecificUserAsync(string buyerEmail, int orderId);
        Task<IEnumerable<Order>> GetOrdersForSpecificUserAsync(string buyerEmail);
    }
}
