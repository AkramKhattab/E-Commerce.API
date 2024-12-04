using Ecommerce.Store.APIs.Errors;
using Ecommerce.Store.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Ecommerce.Store.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }   

        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<IActionResult> CreatePaymentIntent (string basketId)
        {
            if(basketId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var basket = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);
            if(basket is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(basket);
        }


        // This is your Stripe CLI webhook secret for testing your endpoint locally.
        const string endpointSecret = "whsec_ec31abee44338d2128244c638bb2c6e88c44734573d8c4a2911675d776002e06";

        [HttpPost("webhook")] // https://localhost:7205//api/payments/webhook
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;

                // Handle the event
                if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    // Update DB
                    await _paymentService.UpdatePaymentIntentForSucceedOrFailed(paymentIntent.Id, false);
                }
                else if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    // Update DB
                    await _paymentService.UpdatePaymentIntentForSucceedOrFailed(paymentIntent.Id, true);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }








    }
}
