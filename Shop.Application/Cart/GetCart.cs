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

        public Response Do()
        {
            var session = _session.GetString("Cart");
            var cartProduct = JsonConvert.DeserializeObject<CartProduct>(session);

            var response = _ctx.Stock
                .Include(x=>x.Product)
                .Where(x => x.Id.Equals(cartProduct.StockId))
                .Select(y => new Response()
                {
                    StockId = y.Id,
                    Qty=cartProduct.Qty,
                    Name = y.Product.Name,
                    Value = y.Product.Value.ToString()
                }).FirstOrDefault();

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
