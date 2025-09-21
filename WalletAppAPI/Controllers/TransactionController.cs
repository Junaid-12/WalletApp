using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletAppAPI.Repository;
using WalletAppAPI.Model;
using Microsoft.AspNetCore.Authorization;
namespace WalletAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransecationRepository _transactionRepo;
        private readonly EmailNotificationRepository _emailNotify;

        public TransactionController(ITransecationRepository transactionRepo, EmailNotificationRepository emailNotify)
        {
            _transactionRepo = transactionRepo;
            _emailNotify = emailNotify;
        }

        [HttpPost("SendMoney")]
        public async Task<IActionResult> SendMoney([FromBody] Transaction transaction)
        {
            var result = await _transactionRepo.SendMoneyAsync(transaction);
            if( !result)
            {
                return BadRequest(new { message = "Sender wallet not found or balance not set" });
            }
             await _emailNotify.ReciveEmailUser(transaction);
            return Ok(new { message=" Transaction Sucessfully"});
        }
      
        [HttpGet("UserTransactions/{userId}")]
        public async Task<IActionResult> GetUserTransactions(int userId)
        {
            var result = await _transactionRepo.GetUserTransactionsAsync(userId);
            return Ok(result);
        }

    }
}
