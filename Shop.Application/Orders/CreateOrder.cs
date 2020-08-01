using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Orders
{
    public class CreateOrder
    {
        private ApplicationDbContext _ctx;

        public CreateOrder(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> Do(Request request)
        {
            // Decrease the stock from the warehouse first
            var stocksToUpdate = _ctx.Stock
                .AsEnumerable()
                .Where(x => request.Stocks.Any(y => y.StockId.Equals(x.Id))).ToList();
            foreach(var stock in stocksToUpdate)
            {
                stock.Qty = stock.Qty - request.Stocks.FirstOrDefault(x => x.StockId.Equals(stock.Id)).Qty;
            }            
            var order = new Order
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNo = request.PhoneNo,
                Address1 = request.Address1,
                Address2 = request.Address1,
                City = request.City,
                PostCode = request.PostCode,
                OrderStocks = request.Stocks.Select(x => new OrderStock
                {
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList(),
                StripeReference = request.StripRef,
                OrderRef = CreateOrderRef()

            };
            _ctx.Orders.Add(order);
            return await _ctx.SaveChangesAsync() > 0;           
        }

        public class Request
        {
            public string StripRef { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNo { get; set; }

            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
            public List<Stock> Stocks { get; set; }
        }

        public class Stock
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public string CreateOrderRef()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString(); ;
        }
    }
}
