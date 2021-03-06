﻿using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.ProductsAdmin
{
    public class GetProducts
    {
        private ApplicationDbContext _context;
        public GetProducts(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductViewModel> Do()
        {
            //$"£ {x.Value:N2}"
            return _context.Products.ToList().Select(x=> new ProductViewModel 
            { 
                Name= x.Name,
                Description = x.Description,                
                Value = x.Value
            });
        }
        public class ProductViewModel
        {
            public string Name { get; set; }
            public string Description { get; set; }            
            public decimal Value { get; set; }
        }
    }


}
