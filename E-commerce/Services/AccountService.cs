using E_commerce.DTOS;
using E_commerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_commerce.Services
{
    public class AccountService
    {
        private SignInManager<ApplicationUser> signInManager;
        private UserManager<ApplicationUser> userManager;
        private readonly IConfiguration Configuration;

        public AccountService(SignInManager<ApplicationUser> _singInManager , UserManager<ApplicationUser> _userManager,IConfiguration _Configuration)
        {
            signInManager = _singInManager;
            userManager = _userManager;
            Configuration = _Configuration;
        }

        public async Task<string> LoginAsync(UserLoginDto userloginDto)
        {
                var appuser = await userManager.FindByEmailAsync(userloginDto.email);
                if (appuser == null || ! await userManager.CheckPasswordAsync(appuser, userloginDto.password))
                {
                    throw new UnauthorizedAccessException("Invalid credentials");
                }

            return GenerateJwtToken(appuser);

        }

        public async Task<string> RegisterAsync(UserRegisterDto userRegisterDto)
        {

            var existingUser = await userManager.FindByEmailAsync(userRegisterDto.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already in use");
            }

            var appUser = new ApplicationUser
            {
                UserName = userRegisterDto.UserName,
                Email = userRegisterDto.Email,
            };

            var result = await userManager.CreateAsync(appUser, userRegisterDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
            }

            return GenerateJwtToken(appUser);
        }

        private string GenerateJwtToken(ApplicationUser appUser)
        {
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                    new Claim(ClaimTypes.Email, appUser.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }


    }
}
