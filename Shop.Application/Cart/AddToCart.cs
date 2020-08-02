using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Shop.Database;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public AddToCart(ISession session,ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public async Task<bool> Do(Request request)
        {
            var stockOnHold = _ctx.Stock.Where(x => x.Id.Equals(request.StockId)).FirstOrDefault();
            
            // no quantity left in warehouse
            if (stockOnHold.Qty < request.Qty) return false;
            
            _ctx.StockOnHolds.Add(new StockOnHold
            {
                StockId = request.StockId,
                Qty = request.Qty,
                ExpiryDate = DateTime.Now.AddMinutes(20)
            });
            stockOnHold.Qty = stockOnHold.Qty - request.Qty;

            await _ctx.SaveChangesAsync();

            var cartList = new List<CartProduct>();
            var session = _session.GetString("Cart");
            if (!string.IsNullOrEmpty(session))
            {
                cartList = JsonConvert.DeserializeObject<List<CartProduct>>(session);
            }

            if (cartList.Any(x=>x.StockId.Equals(request.StockId)))
            {
                cartList.Find(x => x.StockId.Equals(request.StockId)).Qty += request.Qty;
            }
            else
            {
                cartList.Add(new CartProduct
                {
                    StockId = request.StockId,
                    Qty = request.Qty
                });
            }
            
            var value=JsonConvert.SerializeObject(cartList);
            _session.SetString("Cart", value);

            return true;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
