using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulsakuService.DTOs.Transaction;
using PulsakuService.Services;
using System.Security.Claims;

namespace PulsakuService.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(
            ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateTransactionRequest request)
        {
            try
            {
                var userId = GetUserId();

                var result = await _transactionService.CreateAsync(
                    userId,
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

        [HttpGet]
        public async Task<IActionResult> GetMyTransactions()
        {
            var userId = GetUserId();

            var result =
                await _transactionService.GetMyTransactionsAsync(userId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var userId = GetUserId();

                var result = await _transactionService.GetByIdAsync(
                    userId,
                    id
                );

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
        }

        private long GetUserId()
        {
            var userIdClaim =
                User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new Exception("UserId not found in token");
            }

            return long.Parse(userIdClaim.Value);
        }
    }
}