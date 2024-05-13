using E_commerce.DTOS;
using E_commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace E_commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> _userManager,SignInManager<ApplicationUser>_signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserDto user)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var appuser = new ApplicationUser()
            {
                UserName = user.UserName,
                Email = user.Email,

            };

            var result = await userManager.CreateAsync(appuser,user.Password);

            if (result.Succeeded) 
            {
                return Ok("Registiration Succeeded");
            }

            else
            {
                return BadRequest(result.Errors);
            }

        }



    }
}
