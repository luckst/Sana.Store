using MediatR;

namespace Sana.Store.Application.Queries.Catalog
{
    public class GetProductsQueryHandler
    {
        public class Query : IRequest<Unit>
        {
        }

        public class Handler : IRequestHandler<Query, Unit>
        {           

            public async Task<Unit> Handle(
                Query query,
                CancellationToken cancellationToken
            )
            {
                return new Unit();
            }
        }
    }
}
