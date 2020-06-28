using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class CreateProduct
    {
        private ApplicationDbContext _context;
        public CreateProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProductModelResponse> Do(ProductModelRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductModelResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Value = product.Value
            };
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
