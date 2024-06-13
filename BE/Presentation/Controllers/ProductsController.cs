using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Threading;
using ViewModels;
using ViewModels.DataBase;

namespace Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("Products")]
    [Produces("text/plain")]
    public class ProductsController : ControllerBase
    {
        public IProductServices _productServices;
        public IEmailExtend _emailExtend;
       public ProductsController(IProductServices productServices, IEmailExtend emailExtend) {
            _productServices = productServices;
            _emailExtend = emailExtend;
       }
        [HttpGet]
        [Route("get-list-data")]

        public async Task<IActionResult> GetListData() {
            try
            {
                var rs = await _productServices.GetDataService();
                if (rs.Count == 0) return Ok(null);

                return Ok(JsonSerializer.Serialize(rs));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("get-by-id")]
        public IActionResult GetDataByID(int id)
        {
            try
            {
                var rs = _productServices.GetDataByIdService(id);
                if (rs == null)   return Ok(null);

                return Ok(JsonSerializer.Serialize(rs));
            }
            catch (Exception )
            {
                throw ;
            }

        }
        [HttpPost("save-product-by-id")]
        public IActionResult SaveProductById([FromBody] ProductsModel model)
        {
            var result = _productServices.SaveProductByIdService(model);
            return Ok(JsonSerializer.Serialize(result));
        }
        [HttpPut("save-list-product")]
        public IActionResult SaveOrder([FromBody] List<ProductsModel> models)
        {
            var rs = _productServices.SaveListProduct(models);
            return Ok(JsonSerializer.Serialize(rs));
        }

        [HttpGet("get-categories-with-cancel")]
        public async Task<IActionResult> GetProductWithCancel(CancellationToken cancellation)
        {
            var rs = (await _productServices.GetProductWithCancel(cancellation));
            return Ok(JsonSerializer.Serialize(rs));
          
        }
        [HttpPost("send-mail")]
        public async Task<IActionResult> SendMail(CancellationToken cancellation, EmailRequest emailrq)
        {
            await _emailExtend.SendEmailAsync(cancellation, emailrq);
            return Ok();
        }
    }
}
