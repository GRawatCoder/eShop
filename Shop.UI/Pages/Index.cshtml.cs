using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Shop.Application.ProductsAdmin;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _ctx;       

        public IEnumerable<GetProducts.ProductViewModel> Products { get; set; }

        public IndexModel(ILogger<IndexModel> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _ctx = context;
        }
        
        public void OnGet()
        {
            Products = new GetProducts(_ctx).Do(); 
        }
        
    }
}
