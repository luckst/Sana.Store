using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Sana.Store.Entities.Dtos;
using Sana.Store.Entities.Settings;
using System.Data;

namespace Sana.Store.Application.Queries.Catalog
{
    public class GetProductsQueryHandler
    {
        public class Query : IRequest<List<ProductDto>>
        {
            public int PageNumber { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<ProductDto>>
        {
            private readonly GlobalSettings _globalSettings;

            public Handler(GlobalSettings globalSettings)
            {
                _globalSettings = globalSettings;
            }

            public async Task<List<ProductDto>> Handle(
                Query query,
                CancellationToken cancellationToken
            )
            {
                using (IDbConnection conn = new SqlConnection(_globalSettings.DbConnectionString))
                {
                    string sql =
                    @"
                    SELECT *
                    FROM 
                    (    
	                    SELECT ROW_NUMBER() OVER ( ORDER BY p.Title ) as RowNum,
                        p.Id,
                        p.Title,
                        p.Code,
                        p.Description,
                        p.Price,
                        p.AvailableStock
                        FROM Products p
                    ) AS RowResult
                    WHERE RowNum >= ((@NumPage - 1) * @Size) + 1
                    AND RowNum <= (@NumPage * @Size)
                    ORDER BY RowNum
                    ";

                    var result = await conn.QueryAsync<ProductDto>(sql, new
                    {
                        NumPage = query.PageNumber,
                        Size = 10
                    });

                    return result.ToList();
                }
            }
        }
    }
}
