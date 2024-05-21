using MediatR;
using ProductAPI.Application.Products.Commands;
using ProductAPI.Core.Interfaces;
using ProductAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Application.Products.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);
            if (product == null || product.CreatedBy != request.CreatedBy)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this product.");
            }

            await _repository.DeleteProductAsync(product);

            return Unit.Value;
        }
    }
}
