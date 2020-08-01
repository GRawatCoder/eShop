using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {
        private IHostEnvironment _env;

        [BindProperty]
        public AddCustomerInformation.Request CustomerInfo { get; set; }

        public CustomerInformationModel(IHostEnvironment env)
        {
            _env = env;
        }
        
        public IActionResult OnGet()
        {
            var customerInformation = new GetCustomerInformation(HttpContext.Session).Do();
            if (customerInformation == null) {
                if (_env.IsDevelopment())
                {
                    CustomerInfo = new AddCustomerInformation.Request
                    {
                        FirstName = "A",
                        LastName = "A",
                        Address1 = "A",
                        Address2 = "A",
                        City = "A",
                        Email = "g@gmail.com",
                        PhoneNo = "222",
                        PostCode = "A"
                    };
                }
                return Page();
            } 

            return RedirectToPage("Payment");
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();
            new AddCustomerInformation(HttpContext.Session).Do(CustomerInfo);
            return RedirectToPage("Payment");
        }
    }
}