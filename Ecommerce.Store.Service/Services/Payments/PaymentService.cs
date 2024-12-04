using Ecommerce.Store.Core;
using Ecommerce.Store.Core.Dtos.Baskets;
using Ecommerce.Store.Core.Entities;
using Ecommerce.Store.Core.Entities.Order;
using Ecommerce.Store.Core.Repositories.Contract;
using Ecommerce.Store.Core.Services.Contract;
using Ecommerce.Store.Core.Specifications.Orders;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Ecommerce.Store.Core.Entities.Product;

namespace Ecommerce.Store.Service.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketService _basketService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(IBasketService basketService, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _basketService = basketService;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<CustomerBasketDto> CreateOrUpdatePaymentIntentIdAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            // Get Basket
            var basket = await _basketService.GetBasketAsync(basketId);
            if (basket is null) return null;

            var shippingPrice = 0m;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
                shippingPrice = deliveryMethod.Cost;
            }

            if(basket.Items.Count() > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product, int>().GetAsync(item.Id);
                    if (item.Price != product.Price)
                    {
                        item.Price = product.Price;
                    }
                }
            }


            var subTotal = basket.Items.Sum(I => I.Price * I.Quantity);


            var service = new PaymentIntentService();


            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                // Create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long) (subTotal * 100 + shippingPrice * 100),
                    PaymentMethodTypes = new List<string>() { "Card"},
                    Currency = "usd"
                };

                paymentIntent = await service.CreateAsync(options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)(subTotal * 100 + shippingPrice * 100),
                };

                paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, options);
                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }



            basket = await _basketService.UpdateBasketAsync(basket);
            if (basket is null) return null;
            return basket;




        }

        public async Task<Order> UpdatePaymentIntentForSucceedOrFailed(string paymentIntentId, bool flag)
        {

            var spec = new OrderSpecificationWithPaymentIntentId(paymentIntentId);
            var order = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
            if (flag)
            {
                order.Status = OrderStatus.PaymentReceived;
            }
            else
            {
                order.Status = OrderStatus.PaymentFailed;
            }

            _unitOfWork.Repository<Order, int>().Update(order);
            await _unitOfWork.CompleteAsync();

            return order;

        }
    }
}
