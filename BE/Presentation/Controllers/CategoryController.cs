using IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ViewModels;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("Category")]
    [Produces("text/plain")]
    public class CategoryController : Controller
    {
        public ICategoriesServices _categoriesServices;
        public CategoryController(ICategoriesServices categoriesServices) {
            _categoriesServices = categoriesServices;
        } 
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> GetCate([FromForm] QueryModel queryModel)
        {
            var result = await _categoriesServices.GetDataService(queryModel);
            return Ok(JsonSerializer.Serialize(result));
        }
    }
}
