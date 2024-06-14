using E_commerce.DTOS;
using E_commerce.Models;
using E_commerce.Services;
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
        private readonly AccountService accountService;

        public AccountController(AccountService _accountService)
        {
            accountService = _accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await accountService.RegisterAsync(user);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto user)
        {

            if (!ModelState.IsValid)
            {
                
                return BadRequest(ModelState);

            }
            try
            {
                var token = await accountService.LoginAsync(user);

                return Ok(new { Token = token });
            }

            catch (UnauthorizedAccessException ex) 
            {
                return Unauthorized(ex.Message);
            
            }

            catch(Exception ex) 
            { 
                return BadRequest(ex.Message);
            
            }



        }
    }
}
