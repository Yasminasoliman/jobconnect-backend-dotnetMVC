using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using jobconnect.Data;
using jobconnect.Dtos;
using jobconnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

//AuthController about login and register users by Radwa Khaled

namespace jobconnect.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }
  /*********************************************************** Login **********************************************************/
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var userInData = await _authRepository.Login(loginDto.Email, loginDto.Password);
            //if user not in data
            if (userInData == null)
                return Unauthorized();
            //else 
            var token = GenerateJwtToken(userInData);
            return Ok(new{token});
        }

  /*********************************************************** Register **********************************************************/

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            //if user in data already
            if (await _authRepository.UserExist(userDto.Email))
                return BadRequest("User already Exists");
            //else
            var CreateUser = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                UserType = userDto.UserType 
            };

            var createdUser = await _authRepository.Register(CreateUser, userDto.Password);

            if (createdUser != null)
                return StatusCode(200); 

            return StatusCode(500); 
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserType)
                  
            };

            // size of key
            var key = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(key);
            }

            var securityKey = new SymmetricSecurityKey(key);

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}





