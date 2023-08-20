using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CategoriesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var data = await _serviceManager.CategoryService.GetAllCategoriesAsync(false);
            return Ok(data);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAllCategories([FromRoute] int id)
        {
            var data = await _serviceManager.CategoryService.GetOneCategoryIdAsync(id,false);
            return Ok(data);
        }
    }
}
