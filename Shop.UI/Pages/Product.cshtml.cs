﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Products;
using Shop.Database;
using static Shop.Application.Products.GetProduct;

namespace Shop.UI.Pages
{
    public class ProductModel : PageModel
    {
        private ApplicationDbContext _ctx;
        public ProductViewModel Product { get; set; }

        [BindProperty]
        public Test ProductTest { get; set; }

        public class Test
        {
            public string Id { get; set; }
        }

        public ProductModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public IActionResult OnGet(string name)
        {
            Product = new GetProduct(_ctx).Do(name);
            if (Product == null)
                return RedirectToPage("Index");
            return Page();
        }
        public IActionResult OnPost()
        {            
            var currentId = HttpContext.Session.GetString("id");
            HttpContext.Session.SetString("id", ProductTest.Id);
            return RedirectToPage("Index");
        }
    }
}