using Microsoft.EntityFrameworkCore;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Products
{
    public class GetProduct
    {
        private ApplicationDbContext _context;
        public GetProduct(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProductViewModel Do(string name)
        {
            //$"£ {x.Value:N2}"
            return _context.Products
                .Include(x=>x.Stock)
                .Where(x=>x.Name.Equals(name))
                .Select(x => new ProductViewModel
                {                
                    Name = x.Name,
                    Description = x.Description,
                    Value = x.Value,
                    Stocks= x.Stock.Select(y => new StockViewModel
                    {
                        Id = y.Id,
                        Description = y.Description,
                        IsStockAvailable = y.Qty<1?false:true
                    })                    
                }).FirstOrDefault();
        }
        public class ProductViewModel
        {         
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }
            public IEnumerable<StockViewModel> Stocks { get; set; }

        }
        public class StockViewModel
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public bool IsStockAvailable { get; set; }
            

        }
    }
}
