using GameShopping.Web.Models;
using GameShopping.Web.Services.IServices;
using GameShopping.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameShopping.Web.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<IActionResult> ProductIndex()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);
                if (response != null) return RedirectToAction(
                     nameof(ProductIndex));
            }
            return View(model);
        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> ProductUpdate(int id)
        {
            var model = await _productService.GetProductByIdAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProduct(model);
                if (response != null) return RedirectToAction(
                     nameof(ProductIndex));
            }
            return View(model);
        }
        [Authorize(Roles =Role.Admin)]
        [HttpDelete]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var model = await _productService.GetProductByIdAsync(id);
            if (model != null) return View(model);
            return NotFound();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var response = await _productService.DeleteProductById(model.Id);
            if (response) return RedirectToAction(
                    nameof(ProductIndex));
            return View(model);
        }
    }
}