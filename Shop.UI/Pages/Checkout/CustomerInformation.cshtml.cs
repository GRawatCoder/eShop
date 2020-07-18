using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {        
        [BindProperty]
        public AddCustomerInformation.Request CustomerInfo { get; set; }
        
        public IActionResult OnGet()
        {
            var customerInformation = new GetCustomerInformation(HttpContext.Session).Do();
            if (customerInformation == null) return Page();

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