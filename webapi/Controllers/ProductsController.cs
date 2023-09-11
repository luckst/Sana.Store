using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sana.Store.Application.Queries.Catalog;
using Sana.Store.Entities.Dtos;
using System.Net.Http.Headers;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<List<ProductDto>> GetProducts(int pageNumber) 
        {
            var query = new GetProductsQueryHandler.Query
            {
                PageNumber = pageNumber
            };
            return await _mediator.Send(query);
        }

        [HttpGet("{productId}/stockavailable/{quantity}")]
        public async Task<IActionResult> ValidateAvailableStock([FromRoute] Guid productId, [FromRoute] int quantity)
        {
            var query = new TheresStockAvailableQueryHandler.Query
            {
                ProductId = productId,
                Quantity = quantity
            };
            return Ok(new { IsAvailable = await _mediator.Send(query) });
        }
    }
}
