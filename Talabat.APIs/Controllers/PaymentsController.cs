using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Talabat.APIs.Errors;
using Talabat.Core.DTOs;
using Talabat.Core.Interfaces.Services;

namespace Talabat.APIs.Controllers
{

    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        const string endpointSecret = "whsec_bd46d0722966d25a29f612bcdf6ab1faec3091cc98df18dabd774639df3b1f39";
        public PaymentsController(IPaymentService paymentService, IMapper mapper)
        {
            _paymentService = paymentService;
            _mapper = mapper;
        }
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [HttpPost("CreateOrUpdatePaymentIntent")]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
          var result = await  _paymentService.CreateOrUpdatePaymentIntentId(basketId);
          if (result is null) return BadRequest(new ApiResponse(400,"There is A problem With Your Basket"));
          var resultDto = _mapper.Map<CustomerBasketDto>(result);
          return Ok(resultDto);
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> StripWebHook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, 
                                  Request.Headers["Stripe-Signature"],endpointSecret);
                var PaymentIntent = stripeEvent.Data.Object as PaymentIntent;

                // Handle the event
                if (stripeEvent.Type == EventTypes.PaymentIntentPaymentFailed)
                {
                    await _paymentService.UpdatePaymentIntentToSucceededOrFailed(PaymentIntent.Id, false);
                }
                else if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
                {
                    await _paymentService.UpdatePaymentIntentToSucceededOrFailed(PaymentIntent.Id, true);
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
