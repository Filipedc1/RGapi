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
        public async Task<ActionResult> RegisterAsync(RegisterViewModel model, bool isSalon)
        {
            var hasher = new PasswordHasher<AppUser>();

            // if a salon, put address within the salon table
            var user = new AppUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber,
                Address = (isSalon == false) ? BuildAddress(model) : null,
                Salon = (isSalon == true) ? BuildSalon(model) : null,
                MemberSince = DateTime.Now
            };

            user.PasswordHash = hasher.HashPassword(user, model.Password);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var claims = new List<Claim>
                {
                    new Claim("username", user.UserName),
                    new Claim("role", (isSalon == true) ? "Salon" : "Customer") 
                };

                await _userManager.AddClaimsAsync(user, claims);

                return Ok();
            }

            return BadRequest();
        }

        // USED FOR TESTING AUTHORIZATION
        [Authorize("Admin")]
        [HttpPost("delete")]
        public ActionResult Delete()
        {
            return Ok();
        }

        private async Task<string> GenerateJwtToken(AppUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

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

        #region Helpers

        private Address BuildAddress(RegisterViewModel model)
        {
            return new Address
            {
                Street = model.Street,
                City = model.City,
                State = model.State,
                Zip = model.Zip,
            };
        }

        private Salon BuildSalon(RegisterViewModel model)
        {
            return new Salon
            {
                Name = model.SalonName,
                License = model.SalonLicense,
                PhoneNumber = model.PhoneNumber,
                Address = BuildAddress(model)
            };
        }

        #endregion
    }
}