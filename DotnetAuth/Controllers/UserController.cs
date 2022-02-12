using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetAuth.Data.Entitites;
using DotnetAuth.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotnetAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(ILogger<UserController> logger,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
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
                    return await Task.FromResult("Login Successfully.");
                }

                return await Task.FromResult("Invalid Email or Password");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }
    }
}
