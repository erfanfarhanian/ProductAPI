using AutoMapper;
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
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (product.CreatedBy != request.CreatedBy)
            {
                throw new UnauthorizedAccessException("You are not authorized to update this product.");
            }

            //product.Name = request.Name;
            //product.ManufacturePhone = request.ManufacturePhone;
            //product.ManufactureEmail = request.ManufactureEmail;
            //product.ProduceDate = request.ProduceDate;
            //product.IsAvailable = request.IsAvailable;

            _mapper.Map(request, product);

            await _repository.UpdateProductAsync(product);

            return Unit.Value;
        }
    }
}
