using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulsakuService.DTOs.Payment;
using PulsakuService.Services;
using System.Security.Claims;

namespace PulsakuService.Controllers
{
    [ApiController]
    [Route("api/payments")]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{transactionId}/pay")]
        public async Task<IActionResult> Pay(
            long transactionId,
            PayTransactionRequest request)
        {
            try
            {
                var userIdClaim =
                    User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                var userId = long.Parse(userIdClaim.Value);

                var result = await _paymentService.PayAsync(
                    userId,
                    transactionId,
                    request
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}