using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class CreateStock
    {
        private ApplicationDbContext _ctx;

        public CreateStock(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<StockModelResponse> Do(StockModelRequest request)
        {
            var stock = new Stock()
            {
                ProductId = request.ProductId,
                Description = request.Description,
                Qty = request.Qty
            };
            _ctx.Stock.Add(stock);
            await _ctx.SaveChangesAsync();
            return new StockModelResponse
            {
                Id= stock.Id,
                Description = stock.Description,
                Qty = stock.Qty
            };
        }

        public class StockModelRequest
        {
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }

        }
        public class StockModelResponse
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }

        }
    }

    

}
