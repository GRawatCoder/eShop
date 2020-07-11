using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductsAdmin;
using Shop.Application.StockAdmin;
using Shop.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    [Route("{controller}")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _ctx;
        public AdminController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        #region products
        [HttpGet("products")]
        public ActionResult GetProducts()
        {
            return Ok(new GetProducts(_ctx).Do());
        }
        [HttpGet("products/{Id}")]
        public ActionResult GetProduct(int Id)
        {
            return Ok(new GetProduct(_ctx).Do(Id));
        }
        [HttpDelete("products/{Id}")]
        public async Task<ActionResult> DeleteProduct(int Id)
        {
            return Ok(await new DeleteProduct(_ctx).Do(Id));
        }
        [HttpPost("products")]
        public async Task<ActionResult> CreateProducts([FromBody] CreateProduct.ProductModelRequest productModelRequest)
        {
            return Ok(await new CreateProduct(_ctx).Do(productModelRequest));
        }
        [HttpPut("products")]
        public async Task<ActionResult> UpdateProducts([FromBody] UpdateProduct.Request request)
        {
            return Ok(await new UpdateProduct(_ctx).Do(request));
        }
        #endregion

        #region stock
        [HttpGet("stocks")]
        public ActionResult GetStocks()
        {
            return Ok(new GetStock(_ctx).Do());
        }
        [HttpPost("stocks")]
        public async Task<ActionResult> CreateStock([FromBody] CreateStock.StockModelRequest request)
        {
            return Ok(await new CreateStock(_ctx).Do(request));
        }
        [HttpDelete("stocks/{Id}")]
        public async Task<ActionResult> DeleteStock(int Id)
        {
            return Ok(await new DeleteStock(_ctx).Do(Id));
        }        
        [HttpPut("stocks")]
        public async Task<ActionResult> UpdateStock([FromBody] UpdateStock.StockModelRequest request)
        {
            return Ok(await new UpdateStock(_ctx).Do(request));
        }
        #endregion


    }
}
