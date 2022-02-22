using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DotnetAuth.Data;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTConfig _jwtConfig;

        public UserController(ILogger<UserController> logger,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IOptions<JWTConfig> jwtConfig,
        RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtConfig = jwtConfig.Value;
            _roleManager = roleManager;
        }

        [Authorize()]
        [HttpPost("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(registerDTO.Email))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Email is required.", null));
                }
                else if (string.IsNullOrEmpty(registerDTO.Password))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Password is required.", null));
                }
                if (!await _roleManager.RoleExistsAsync(registerDTO.Role))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Role does not exist.", null));
                }
                AppUser user = new AppUser();
                user.FullName = registerDTO.FullName;
                user.UserName = registerDTO.Email;
                user.Email = registerDTO.Email;
                user.DateCreated = DateTime.Now;
                user.DateModified = DateTime.Now;

                IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);

                if (result.Succeeded)
                {
                    var tempUser = await _userManager.FindByEmailAsync(registerDTO.Email);
                    await _userManager.AddToRoleAsync(tempUser, registerDTO.Role);
                    return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "User has been created.", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, string.Join(',', result.Errors.Select(x => x.Description).ToArray()), null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        [Authorize()]
        [HttpGet("GetAllUser")]
        public async Task<object> GetAllUser()
        {
            try
            {
                List<UserDTO> userDTOs = new List<UserDTO>();
                List<AppUser> users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    string role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    userDTOs.Add(new UserDTO(user.FullName, user.Email, user.Email, user.DateCreated, user.DateModified, role));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "Get All Users.", userDTOs));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        [Authorize()]
        [HttpGet("GetUser")]
        public async Task<object> GetUser()
        {
            try
            {
                List<UserDTO> userDTOs = new List<UserDTO>();
                List<AppUser> users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    string role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                    if (role == "User")
                    {
                        userDTOs.Add(new UserDTO(user.FullName, user.Email, user.Email, user.DateCreated, user.DateModified, role));
                    }
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "Get All Users.", userDTOs));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.Email))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Email is required.", null));
                }
                else if (string.IsNullOrEmpty(loginDTO.Password))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Password is required.", null));
                }

                Microsoft.AspNetCore.Identity.SignInResult result
                = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);

                if (result.Succeeded)
                {
                    AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.Email);
                    string role = (await _userManager.GetRolesAsync(appUser)).FirstOrDefault();
                    UserDTO user = new UserDTO(appUser.FullName, appUser.Email, appUser.Email, appUser.DateCreated, appUser.DateModified, role);
                    user.Token = GenerateToken(appUser);
                    return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "Login successfull.", user));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "Invalid Email or Password.", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Ok, ex.Message, null));
            }
        }

        [Authorize()]
        [HttpPost("AddRole")]
        public async Task<object> AddRole([FromBody] RoleDTO roleDTO)
        {
            try
            {
                if (roleDTO == null && string.IsNullOrEmpty(roleDTO.Name))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Role is required.", null));
                }
                if (await _roleManager.RoleExistsAsync(roleDTO.Name))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Error, "Role is already exist.", null));
                }
                IdentityRole role = new IdentityRole();
                role.Name = roleDTO.Name;

                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "Role has been created.", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, string.Join(',', result.Errors.Select(x => x.Description).ToArray()), null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
            }
        }

        [Authorize()]
        [HttpGet("GetAllRole")]
        public async Task<object> GetAllRole()
        {
            try
            {
                var roles = _roleManager.Roles
                .Select(x => new RoleDTO(x.Name));
                return await Task.FromResult(new ResponseModel(ResponseCode.Ok, "Get All Roles.", roles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.Error, ex.Message, null));
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
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
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
