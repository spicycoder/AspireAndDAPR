using AspireWithDapr.ServiceDefaults;
using AspireWithDapr.ServiceDefaults.Models;
using AspireWithDapr.ServiceDefaults.Requests;
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

        [HttpGet("read")]
        public async Task<ActionResult<Order?>> ReadOrder(string userName)
        {
            var order = await _daprClient.GetStateAsync<Order>(
                Constants.StateStoreName,
                userName);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("order")]
        public async Task<IActionResult> PlaceOrder(OrderRequest request)
        {
            var product = await _daprClient.InvokeMethodAsync<Product>(
                HttpMethod.Get,
                Constants.ProductsApiName,
                $"/api/Products/product?id={request.ProductId}");

            Order order = new()
            {
                Product = product,
                Quantity = request.Quantity,
                UserName = request.Username
            };

            await _daprClient.SaveStateAsync<Order>(
                Constants.StateStoreName,
                order.UserName,
                order);

            await _daprClient.PublishEventAsync(
                Constants.PubSubName,
                Constants.OrderTopic,
                order);

            return Ok();
        }
    }
}
