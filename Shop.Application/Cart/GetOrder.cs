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
    public class GetOrder
    {
        private ISession _session;
        private ApplicationDbContext _ctx;

        public GetOrder(ISession session, ApplicationDbContext ctx)
        {
            _session = session;
            _ctx = ctx;
        }

        public Response Do()
        {
            var cartSession = _session.GetString("Cart");           
            var cartList = JsonConvert.DeserializeObject<List<CartProduct>>(cartSession);

            var listOfProducts = _ctx.Stock
                .Include(x => x.Product)
                .AsEnumerable()
                .Where(x => cartList.Any(y => y.StockId.Equals(x.Id)))
                .Select(x => new Product
                {
                    ProductId = x.ProductId,
                    StockId = x.Id,
                    Qty = cartList.FirstOrDefault(y=>y.StockId.Equals(x.Id)).Qty,
                    Value = (int) (x.Product.Value * 100)
                }).ToList();

            var customerInfoSession = _session.GetString("customer-info");
            var customerInfo = JsonConvert.DeserializeObject<CustomerInformation>(customerInfoSession);

            return new Response
            {
                Products = listOfProducts,
                Customer = new CustomerInformation
                {
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    Email = customerInfo.Email,
                    City = customerInfo.City,
                    Address1 = customerInfo.Address1,
                    Address2 = customerInfo.Address2,
                    PostCode = customerInfo.PostCode,
                    PhoneNo = customerInfo.PhoneNo
                }
            };
        }

        public class Product
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
            public int ProductId { get; set; }
            public int Value { get; set; }
        }

        public class Response
        {
            public IEnumerable<Product> Products { get; set; }
            public CustomerInformation Customer { get; set; }
            public int GetTotalCharge() => Products.Sum(x => x.Value * x.Qty);
        }

        
    }
}
