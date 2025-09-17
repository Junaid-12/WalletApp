using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletAppAPI.Model;
using WalletAppAPI.Repository;

namespace WalletAppAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenSevice _tokenService;
        public UserController(IUserRepository userRepo, ITokenSevice tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
        }

        [HttpPost("RegisterUser")]
        public  async Task<IActionResult> RegisterUser([FromBody]User user)
        {
            var result = await _userRepo.RegisterUserAsync(user);
            if (result ==null)
            {
                return NotFound(" Something happen");
            }

            return Ok(new { message="data hassbeen Successfully save", data=result});
        }

        [HttpPost("Login")]
         public async Task<IActionResult>login([FromBody] UserLogin user)
        {
            var result = await _userRepo.LoginUserAsync(user);
            if (result != null)
            {
                var token = _tokenService.GenrateToken(user.Username, user.Email);
                return Ok(new { token,result.Id,result.Username });
            }
            return Unauthorized("Invalid credentails");
        }   
    }
}
