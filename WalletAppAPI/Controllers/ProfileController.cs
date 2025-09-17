using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using WalletAppAPI.Model;
using WalletAppAPI.Repository;

namespace WalletAppAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileRepository _ropo;
        public ProfileController(IProfileRepository ropo)
        {
            _ropo=ropo;
        }
        [HttpPost]
        [Route("ProfileData")]
        public async Task<IActionResult> ProfileData([FromForm]ProfileUpload profileUpload)
        {
            if (profileUpload.ImageFile == null)
            {
                return BadRequest(new { message = "Image Not Set " });
            }
                var Imageapth= await (_ropo as ProfileRepository  ).SaveImage(profileUpload.ImageFile);
                var userupload = new Profile
                {

                    UserId=profileUpload.UserId,
                    Name = profileUpload.Name,
                    PhoneNo = profileUpload.PhoneNo,
                    Address = profileUpload.Address,
                    Image = Imageapth
                };

               await _ropo.AddProfile(userupload);
            return Ok( new { message="User Profile Data upload Sucessfully"});
            
        }

        [HttpGet]
        [Route("GetProfile/{UserId}")]
        public async Task<IActionResult> GetProfile(int UserId)
        {
            if(UserId == 0)
            {
                return BadRequest(new { message = "Id Not Found or Something Error" });
            }
            var result = await _ropo.GetProfile(UserId);
            if (result!=null)
            {
                return Ok(new { message = " Profile Data is Fetch ", result });

            }
            return Ok( new {message =" Something Error To Fetch Error "} );
            
        }
    }
}
