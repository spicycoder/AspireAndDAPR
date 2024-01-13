using AspireWithDapr.ServiceDefaults;
using AspireWithDapr.ServiceDefaults.Models;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace CartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly DaprClient _daprClient;

        public CartController(DaprClient daprClient)
        {
            _daprClient = daprClient;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveToCart(Cart cart)
        {
            await _daprClient.SaveStateAsync<Cart>(
                Constants.StateStoreName,
                cart.UserName,
                cart);

            return Ok();
        }

        [HttpGet("read")]
        public async Task<ActionResult<Cart?>> ReadFromCart(string userName)
        {
            var cart = await _daprClient.GetStateAsync<Cart>(
                Constants.StateStoreName,
                userName);

            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }
    }
}
