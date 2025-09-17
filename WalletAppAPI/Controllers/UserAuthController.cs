using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WalletAppAPI.Model;
using WalletAppAPI.Repository;

namespace WalletAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserAuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //[HttpPost("Register")]
        //public async Task<IActionResult> Register(UserRegister user)
        //{
        //    bool success = await _userRepository.RegisterAsync(user);
        //    if (!success)
        //    {
        //        return NotFound("User Already Exists");
        //    }
        //    return Ok("Registerd Succssfully");
        //}

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(UserLogin Model)
        //{
        //    var user = await _userRepository.LoginAsync(Model);
        //    if (user == null) { return Unauthorized("Invalid  Credentials"); }
            
        //    return Ok("Login Successful");
        //}
    }
}
