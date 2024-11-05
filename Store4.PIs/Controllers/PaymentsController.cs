using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store4.Core.Entities;
using Store4.Core.Services.Contract;
using Store4.PIs.Errors;
using Stripe;
using Orders = Store4.Core.Entities.Orders.Order;
using System.Net.NetworkInformation;

namespace Store4.PIs.Controllers
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
		public async Task<ActionResult> CratePaymentIntent(string basketId)
		{
			if (basketId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			var basket = await _paymentService.CreateOrUpdatPaymntIntentIdAsync(basketId);
			if (basket is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			return Ok(basket);
		}

		const string endpointSecret = "sk_test_51QCjHUARPsi5raD6M8quYJkfAJbj9Zmviy1H8sXefipb6pAdpXyo1a9HgLY7tJEdrqCdCZH11wd2oMllgqpoOwk300PNWhOe1G";

			[HttpPost("webhook")]//https://localhost:7006/api/Payments/webhook
			public async Task<IActionResult> WebHook()
		{
				var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
				try
				{
					var stripeEvent = EventUtility.ConstructEvent(json,
						Request.Headers["Stripe-Signature"], endpointSecret);

					var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
					
					// Handle the event
					switch (stripeEvent.Type)
					{
						case "PaymentIntentSucceeded":
						//Update DB
						 await _paymentService.UpdatPaymntIntentForSucceedOrFaildAsync(paymentIntent.Id, true);
							Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
							break;
					    	case "PaymentIntentPaymentFailed":
						     await _paymentService.UpdatPaymntIntentForSucceedOrFaildAsync(paymentIntent.Id, false);
						     Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
							break;
					}

					return Ok();
				}
				catch (StripeException ex)
				{
					return BadRequest();
				}
			}
		}
}

