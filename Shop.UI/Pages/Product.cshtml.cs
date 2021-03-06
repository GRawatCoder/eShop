﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Products;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        private ApplicationDbContext _ctx;
        public GetProduct.ProductViewModel Product { get; set; }

        [BindProperty]
        public AddToCart.Request CartViewModel { get; set; }

        public ProductModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IActionResult> OnGet(string name)
        {
            Product = await new GetProduct(_ctx).Do(name);
            if (Product == null)
                return RedirectToPage("Index");
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var isStockAdded = await new AddToCart(HttpContext.Session, _ctx).Do(CartViewModel);
            if(isStockAdded)
            {
                return RedirectToPage("Cart");
            }
            else
            {
                // warning
                return Page();
            }            
        }
    }
}