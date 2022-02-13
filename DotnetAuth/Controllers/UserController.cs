using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotnetAuth.Data.Entitites;
using DotnetAuth.DTOs;
using DotnetAuth.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DotnetAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly JWTConfig _jwtConfig;

        public UserController(ILogger<UserController> logger,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IOptions<JWTConfig> jwtConfig)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtConfig = jwtConfig.Value;
        }

        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] RegisterDTO userDTO)
        {
            try
            {
                AppUser user = new AppUser();
                user.FullName = userDTO.FullName;
                user.UserName = userDTO.Email;
                user.Email = userDTO.Email;
                user.DateCreated = DateTime.Now;
                user.DateModified = DateTime.Now;

                IdentityResult result = await _userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    return await Task.FromResult("User has been created.");
                }
                return await Task.FromResult(string.Join(',', result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUser()
        {
            try
            {
                var users = _userManager.Users
                .Select(x => new UserDTO(x.FullName, x.Email, x.UserName, x.DateCreated, x.DateModified));
                return await Task.FromResult(users);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.Email))
                {
                    return await Task.FromResult("Email is required.");
                }
                else if (string.IsNullOrEmpty(loginDTO.Password))
                {
                    return await Task.FromResult("Password is required.");
                }

                Microsoft.AspNetCore.Identity.SignInResult result
                = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);

                if (result.Succeeded)
                {
                    AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.Email);
                    UserDTO user = new UserDTO(appUser.FullName, appUser.Email,appUser.Email,appUser.DateCreated, appUser.DateModified);
                    user.Token = GenerateToken(appUser);
                    return await Task.FromResult(user);
                }

                return await Task.FromResult("Invalid Email or Password");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }

        private string GenerateToken(AppUser user)
        {
            
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var issuer = _jwtConfig.Issuer;
            var audience = _jwtConfig.Audience;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FullName)
            };
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = creds,
                Issuer = issuer,
                Audience = audience
            };

            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
