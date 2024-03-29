﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RgApi.Interfaces;
using RgApi.Shared.Models;
using RgApi.Services;
using RgApi.ViewModels;

namespace RgApi.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Fields

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IProduct _productService;

        #endregion

        #region Constructor

        public ProductController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IProduct productService, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _productService = productService;
            _configuration = config;
        }

        #endregion

        #region Methods

        [HttpGet("getproducts")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();

            if (products is null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("getproduct/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> GetProductAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("getallproductcollections")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductCollectionsAsync()
        {
            var collections = await _productService.GetAllProductCollectionsAsync();

            if (collections is null)
                return NotFound();

            return Ok(collections);
        }

        [HttpGet("getproductcollection/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductCollectionAsync(int id)
        {
            var collection = await _productService.GetProductCollectionByIdAsync(id);

            if (collection is null)
                return NotFound();

            return Ok(collection);
        }

        //need to figure out how to add product, and add the collections it will belong to.
        //public async Task<ActionResult> AddProduct(AddProductViewModel model)
        //{
        //    if (model is null)
        //        return NotFound();

        //    var product = new Product
        //    {
        //        Name = model.Name,
        //        Description = model.Description,
        //        ImageUrl = model.ImageUrl,
        //        CustomerPrices = model.CustomerPrices,
        //        SalonPrices = model.SalonPrices
        //    };

        //    await _productService.CreateProductAsync();

        //    return Ok();
        //}

        #endregion
    }
}