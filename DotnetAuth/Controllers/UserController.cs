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
    }
}
