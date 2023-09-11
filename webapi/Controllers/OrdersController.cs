using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sana.Store.Application.Commands.Orders;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Unit> CreateOrder([FromBody] CreateOrderCommandHandler.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}
