using AspireWithDapr.ServiceDefaults.Models;
using Bogus;
using Microsoft.AspNetCore.Mvc;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet("product")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = new Faker<Product>()
                .RuleFor(x => x.Id, _ => id)
                .RuleFor(x => x.Name, x => x.Commerce.ProductName())
                .RuleFor(x => x.Price, x => x.Random.Decimal())
                .Generate();

            return Ok(product);
        }
    }
}
