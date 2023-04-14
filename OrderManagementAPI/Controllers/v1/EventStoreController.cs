using Microsoft.AspNetCore.Mvc;
using OrdersManagement.Domain.Interfaces.Repositories;

namespace OrderManagementAPI.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("eventstore")]
    public class EventStoreController : ControllerBase
    {
        private readonly ILogger<EventStoreController> _logger;

        private readonly IEventStoreRepository _eventRepository;

        public EventStoreController(ILogger<EventStoreController> logger, IEventStoreRepository eventRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        [HttpGet]
        public IActionResult Get()
        {
            var orders = _eventRepository.All();
            return Ok(orders);
        }
    }
}
