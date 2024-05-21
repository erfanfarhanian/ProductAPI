using AutoMapper;
using MediatR;
using ProductAPI.Application.Products.Commands;
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
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //var product = new Product
            //{
            //    Name = request.Name,
            //    CreatedBy = request.CreatedBy,
            //    ProduceDate = request.ProduceDate,
            //    ManufactureEmail = request.ManufactureEmail,
            //    ManufacturePhone = request.ManufacturePhone,
            //    IsAvailable = request.IsAvailable,
            //};
            var product = _mapper.Map<Product>(request);

            await _repository.AddProductAsync(product);

            return product.Id;
        }
    }
}
