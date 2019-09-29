using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RgApi.Interfaces;
using RgApi.Shared.Models;

namespace RgApi.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        #region Fields

        private readonly IShoppingCart _shoppingCartService;

        #endregion

        #region Constructor

        public ShoppingCartController(IShoppingCart shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        #endregion

        #region Methods

        [HttpGet("getcartitems/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CartItem>>> GetShoppingCartItemsAsync(string id)
        {
            var cartItems = await _shoppingCartService.GetAllAsync(id);

            if (cartItems is null)
                return NotFound();

            return Ok(cartItems);
        }

        [HttpGet("getcartitem/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CartItem>> GetShoppingCartItemAsync(int id)
        {
            var product = await _shoppingCartService.GetByIdAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        //[HttpPost("addcartitem/{id}")]
        //[AllowAnonymous]
        //public async Task<ActionResult<bool>> AddShoppingCartItemAsync(int id)
        //{
        //    var product = await _shoppingCartService.GetByIdAsync(id);

        //    if (product is null)
        //        return NotFound();

        //    return Ok(product);
        //}

        #endregion
    }
}