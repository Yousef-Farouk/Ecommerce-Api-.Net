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

            return await GenerateJwtToken(appuser);

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

            await userManager.AddToRoleAsync(appUser, "customer"); 
            return await GenerateJwtToken(appUser);
        }

        private  async Task<string> GenerateJwtToken(ApplicationUser appUser)
        {
            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:SecretKey"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var userRoles = await userManager.GetRolesAsync(appUser);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName,appUser.UserName),
               // new Claim(ClaimTypes.Upn,appUser.Image),
            };

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor

            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }


    }
}
