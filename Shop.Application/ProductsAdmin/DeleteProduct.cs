using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.ProductsAdmin
{
    public class DeleteProduct
    {
        private ApplicationDbContext _context;
        public DeleteProduct(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Do(int Id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id.Equals(Id));
            _context.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        
    }


}
