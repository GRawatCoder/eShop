using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Cart;
using Shop.Database;
using Stripe;


namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        public string PublicKey { get; }
        private ApplicationDbContext _ctx;

        public PaymentModel(IConfiguration config, ApplicationDbContext ctx)
        {
            PublicKey = config["Stripe:PublicKey"].ToString();
            _ctx = ctx;
        }

        public IActionResult OnGet()
        {
            var customerInformation = new GetCustomerInformation(HttpContext.Session).Do();
            if (customerInformation == null) return RedirectToPage("CustomerInformation");

            return Page();
        }

        public IActionResult OnPost(string stripeEmail, string stripeToken)
        {
            var cartOrder = new GetOrder(HttpContext.Session, _ctx).Do();

            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                SourceToken = stripeToken,
                Shipping = new ShippingOptions
                {
                    Name = "Gaurav Rawat",
                    Address = new AddressOptions
                    {
                        City = "New Delhi",
                        Line1 = "sadiq nagar",
                        Line2 = "ansal plaza",
                        Country = "India",
                        PostalCode = "110049",
                        State = "Delhi"
                    },
                    Phone = "9654514162"
                }
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = 500,
                Description = "Sample Charge",
                Currency = "inr",
                CustomerId = customer.Id,
                
            });

            return RedirectToPage("/index");
        }
    }
}