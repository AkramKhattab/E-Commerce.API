using System;
using System.Collections.Generic;

namespace Ecommerce.Store.Core.Entities.Order
{
    public class Order : BaseEntity<int>
    {
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Peding;
        public Address ShippingAddress { get; set; }
        public int DeliveryMethodId { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; }

        // Updated constructor to match the service code
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, List<OrderItem> orderItems, decimal subTotal, string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            DeliveryMethodId = deliveryMethod.Id; // Set the DeliveryMethodId
            Items = orderItems;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
            Status = OrderStatus.Peding;
            OrderDate = DateTimeOffset.Now; // Default value, but can be customized
        }

        // Parameterless constructor for EF Core
        public Order() { }

        // Method to calculate the total
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
    }
}