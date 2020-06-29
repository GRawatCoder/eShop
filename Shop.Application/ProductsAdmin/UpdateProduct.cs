using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class UpdateProduct
    {
        private ApplicationDbContext _context;
        public UpdateProduct(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProductModelResponse> Do(ProductModelRequest request)
        {
            await _context.SaveChangesAsync();
            return new ProductModelResponse();
        }
        public class ProductModelRequest
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }

        }

        public class ProductModelResponse
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Value { get; set; }

        }
    }
}
