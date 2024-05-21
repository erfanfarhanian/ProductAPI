using MediatR;
using ProductAPI.Application.Products.Queries;
using ProductAPI.Core.Entities;
using ProductAPI.Core.Interfaces;
using ProductAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Application.Products.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;

        public GetProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductsAsync();
            if (!string.IsNullOrEmpty(request.CreatedBy))
            {
                products = products.Where(p => p.CreatedBy == request.CreatedBy);
            }

            return products;
        }
    }
}
