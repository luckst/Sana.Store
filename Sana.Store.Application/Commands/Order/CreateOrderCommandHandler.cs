using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Sana.Store.Domain;
using Sana.Store.Entities.Models;
using Sana.Store.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Sana.Store.Application.Commands.Orders
{
    public class CreateOrderCommandHandler
    {
        public class Command : CreateOrderModel, IRequest<Unit>
        {
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly ServiceDbContext _context;
            private readonly IMapper _mapper;

            public Handler(ServiceDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(
                Command command,
                CancellationToken cancellationToken
            )
            {
                if (command.CustomerId == Guid.Empty)
                {
                    throw new ArgumentNullException("customer is required");
                }

                if (command.Details == null || command.Details.Count == 0)
                {
                    throw new ArgumentNullException("Products are required");
                }

                Order order = GetOrder(command);

                // Create an instance of the execution strategy
                var executionStrategy = _context.Database.CreateExecutionStrategy();

                // Use the execution strategy to execute the transaction
                await executionStrategy.Execute(async () =>
                {
                    using (var tran = await _context.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            _context.Orders.Add(order);
                            await _context.SaveChangesAsync();

                            List<Product> updatedProducts = await GetUpdatedProducts(order);

                            _context.Products.UpdateRange(updatedProducts);
                            _context.SaveChanges();

                            await tran.CommitAsync();
                        }
                        catch (Exception)
                        {
                            await tran.RollbackAsync();
                            throw;
                        }
                    }
                });

                return new Unit();
            }

            private Order GetOrder(Command command)
            {
                var order = _mapper.Map<Order>(command);
                order.Id = Guid.NewGuid();
                order.CreatedDate = DateTime.Now;
                order.OrderDetails = new List<OrderDetail>();
                order.Number = _context.Orders.Count() + 1;

                foreach (var detail in command.Details)
                {
                    order.OrderDetails.Add(_mapper.Map<OrderDetail>(detail));
                }

                return order;
            }

            private async Task<List<Product>> GetUpdatedProducts(Order order)
            {
                var updatedProducts = new List<Product>();

                foreach (var detail in order.OrderDetails)
                {
                    var product = await _context.Products.FindAsync(detail.ProductId);

                    product!.AvailableStock -= detail.Quantity;

                    updatedProducts.Add(product);
                }

                return updatedProducts;
            }
        }
    }
}
