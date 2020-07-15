using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Application.Cart
{
    public class GetCart
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public GetCart(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public IEnumerable<Response> Do()
        {
            var session = _session.GetString("Cart");
            if (string.IsNullOrEmpty(session)) return new List<Response>();            
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(session);

            var response = _ctx.Stock
                .Include(x=>x.Product)
                .AsEnumerable()
                .Where(x => cartList.Any(y=>y.StockId.Equals(x.Id)))
                .Select(z => new Response()
                {
                    StockId = z.Id,
                    Qty=cartList.FirstOrDefault(x=>x.StockId.Equals(z.Id)).Qty,
                    Name = z.Product.Name,
                    Value = z.Product.Value.ToString()
                }).ToList();

            return response;
        }

        public class Response
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
            public string Name { get; set; }
            public string Value { get; set; }
        }
    }
}
