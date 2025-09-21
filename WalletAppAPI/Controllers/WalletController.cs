using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletAppAPI.Model;
using WalletAppAPI.Repository;

namespace WalletAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IwalletRepository _walletRepo;
        private readonly EmailNotificationRepository _emailNotify;
        public WalletController(IwalletRepository walletRepo , EmailNotificationRepository emailNotify)
        {
            _walletRepo = walletRepo;
            _emailNotify = emailNotify;
        }
        [HttpPost("AddBalance")]
        public async Task<IActionResult> AddBalance([FromBody] Wallet obj)
        {
            try
            {
                bool result = await _walletRepo.AddBalanceAsync(obj.UserId,obj.Balance);
                if (result)
                {
                    return Ok(new { error = false, message = "Data Add Successfully" });
                    
                }
               // await _emailNotify.SendEmailAsync();
                else
                    return BadRequest(new { error = true, message = "No rows updated — check if the user exists" });
            }

            catch (Exception ex) {

                return StatusCode(500, new { error = true, message = ex.Message });
            }
        }

        [HttpGet("Balance/{userId}")]
        public async Task<IActionResult> GetBalance(int userId)
        {
            var result = await _walletRepo.GetWalletBalanceAsync(userId);
            return Ok(result);
        }
    }
}
