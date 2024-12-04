using Ecommerce.Store.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Store.Core.Dtos.Orders
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }

        public string BuyerEmail { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public string Status   { get; set; }

        public AddressDto ShippingAddress { get; set; }

        public string DeliveryMethod { get; set; }

        public decimal DeliveryMethodCost { get; set; }

        public ICollection<OrderItemDto> Items { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public string? PaymentIntentId { get; set; } = string.Empty;
    }
}
