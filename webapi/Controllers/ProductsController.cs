using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sana.Store.Application.Queries.Catalog;
using Sana.Store.Entities.Dtos;

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
        public async Task<List<ProductDto>> GetProducts(int pageNumber) 
        {
            var query = new GetProductsQueryHandler.Query
            {
                PageNumber = pageNumber
            };
            return await _mediator.Send(query);
        }
    }
}
