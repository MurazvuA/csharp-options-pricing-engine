
using Microsoft.AspNetCore.Mvc;
using PricingEngine.Services;
namespace PricingEngine.API
{


    [ApiController]
    [Route("api/pricing")]
    public class PricingController : ControllerBase
    {
        private readonly PricingService _service = new();

        [HttpPost]
        public IActionResult Price([FromBody] PricingRequest request)
        {
            double price = _service.Price(
                request.Model,
                request.Option,
                request.MarketDataSnapshot);

            return Ok(price);
        }
    }
}
