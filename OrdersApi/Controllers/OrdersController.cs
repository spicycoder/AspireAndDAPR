using AspireWithDapr.ServiceDefaults;
using AspireWithDapr.ServiceDefaults.Models;
using Dapr;
using Microsoft.AspNetCore.Mvc;

namespace OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [Topic(Constants.PubSubName, Constants.OrderTopic)]
        [HttpPost("order")]
        public async Task<IActionResult> PlaceOrder(Cart cart)
        {
            _logger.LogInformation("Placing order for {cart}", cart);

            return Ok();
        }
    }
}
