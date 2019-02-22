using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RgApi.Models;
using RgApi.Services;
using RgApi.ViewModels;

namespace RgApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        #region Fields

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        //private readonly IMapper _mapper;
        private readonly IUser _userService;

        #endregion

        #region Constructor

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUser userService, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _configuration = config;
        }

        #endregion

        #region Methods

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            var user = await _userService.GetByUsernameAsync(model.UserName);

            if (user is null) return NotFound();

            bool result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result) return Unauthorized();

            string token = await GenerateJwtToken(user);

            return Ok(token);
        }

        //Need to determine who is registering (Salon or customer)
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> RegisterAsync(RegisterViewModel model)
        {
            var hasher = new PasswordHasher<AppUser>();

            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                MemberSince = DateTime.Now
            };

            user.PasswordHash = hasher.HashPassword(user, model.Password);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid( ).ToString( )),
                    new Claim(ClaimTypes.Name, user.UserName),
                    //new Claim(ClaimTypes.Role, "CEO") //need to determine what role the person is in
                };

                await _userManager.AddClaimsAsync(user, claims);

                return Ok();
            }

            return BadRequest();
        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(double.Parse(_configuration["Jwt:JwtExpireDays"]));

            var token = new JwtSecurityToken
            (
                issuer: _configuration["Jwt:JwtIssuer"],
                audience: _configuration["Jwt:JwtIssuer"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}