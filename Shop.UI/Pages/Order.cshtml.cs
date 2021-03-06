using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Orders;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class OrderModel : PageModel
    {
        private ApplicationDbContext _ctx;
        
        public GetOrder.Response Order { get; set; }

        public OrderModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public void OnGet(string orderReference)
        {
            Order = new GetOrder(_ctx).Do(orderReference);
            
        }
    }
}
