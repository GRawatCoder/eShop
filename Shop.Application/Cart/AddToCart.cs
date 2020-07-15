using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace Shop.Application.Cart
{
    public class AddToCart
    {
        private ISession _session;
        public AddToCart(ISession session)
        {
            _session = session;
        }

        public void Do(Request request)
        {
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
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
