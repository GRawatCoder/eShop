using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.StockAdmin
{
    public class UpdateStock
    {
        private ApplicationDbContext _ctx;

        public UpdateStock(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<StockModelResponse> Do(StockModelRequest request)
        {
            var stocks = new List<Stock>();
            foreach(StockViewModel stock in request.Stocks)
            {
                stocks.Add(new Stock
                {
                    Id = stock.Id,
                    Description = stock.Description,
                    ProductId = stock.ProductId,
                    Qty=stock.Qty
                });
            }
            _ctx.Stock.UpdateRange(stocks);
            await _ctx.SaveChangesAsync();

            return new StockModelResponse 
            {
                Stocks = request.Stocks
            };
        }

        public class StockViewModel
        {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public string Description { get; set; }
            public int Qty { get; set; }
        }

        public class StockModelRequest
        {
            public IEnumerable<StockViewModel> Stocks { get; set; }

        }
        public class StockModelResponse
        {
            public IEnumerable<StockViewModel> Stocks { get; set; }
        }
    }
}
