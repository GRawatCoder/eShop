using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
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
            var cartProduct = new CartProduct
            {
                StockId = request.StockId,
                Qty = request.Qty
            };
            var value=JsonConvert.SerializeObject(cartProduct);
            _session.SetString("Cart", value);
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
