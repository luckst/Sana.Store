using MediatR;
using Sana.Store.Entities.Settings;
using Sana.Store.Infrastructure;

namespace Sana.Store.Application.Queries.Catalog
{
    public class TheresStockAvailableQueryHandler
    {
        public class Query : IRequest<bool>
        {
            public int Quantity { get; set; }
            public Guid ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Query, bool>
        {
            private readonly GlobalSettings _globalSettings;
            private readonly ServiceDbContext _context;

            public Handler(GlobalSettings globalSettings, ServiceDbContext context)
            {
                _globalSettings = globalSettings;
                _context = context;
            }

            public async Task<bool> Handle(
                Query query,
                CancellationToken cancellationToken
            )
            {
                if (query.ProductId == Guid.Empty)
                {
                    throw new ArgumentNullException("Product id is required");
                }

                if (query.Quantity == 0)
                {
                    throw new ArgumentNullException("Quantity is required");
                }

                var product = await _context.Products.FindAsync(query.ProductId);

                if (product == null)
                {
                    throw new KeyNotFoundException("Product not found");
                }

                if (query.Quantity <= product.AvailableStock)
                    return true;

                return false;
            }
        }
    }
}
