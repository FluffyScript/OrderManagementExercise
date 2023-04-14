using Microsoft.AspNetCore.Mvc;
using OrderManagementApplication;
using OrderManagementApplication.Models;

namespace OrderManagementAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _orderService.GetAll();
            return Ok(orders);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _orderService.GetById(id);
            return Ok(order);
        }

        [HttpGet]
        [Route("pagination")]
        public async Task<IActionResult> Paginated(int skip, int take)
        {
            var orders = await _orderService.GetAll(skip, take);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel orderViewModel)
        {
            await _orderService.Create(orderViewModel);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] OrderViewModel orderViewModel)
        {
            await _orderService.Update(orderViewModel);
            return Ok();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            await _orderService.Cancel(id);
            return Ok();
        }
    }
}