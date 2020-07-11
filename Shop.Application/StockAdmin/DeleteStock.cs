using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class DeleteStock
    {
        private ApplicationDbContext _ctx;

        public DeleteStock(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Do(int Id)
        {
            var stock = _ctx.Stock.FirstOrDefault(x => x.Id.Equals(Id));
            _ctx.Remove(stock);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
