using Ecommerce.Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Dtos.Baskets
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }

    }
}
