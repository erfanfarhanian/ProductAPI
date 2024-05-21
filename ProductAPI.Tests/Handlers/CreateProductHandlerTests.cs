using AutoMapper;
using Moq;
using ProductAPI.Application.Products.Commands;
using ProductAPI.Application.Products.Handlers;
using ProductAPI.Core.Entities;
using ProductAPI.Core.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductAPI.Tests.Handlers
{
    public class CreateProductHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateProductCommand, Product>();
            });

            _mapper = config.CreateMapper();
            _handler = new CreateProductHandler(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Add_Product_And_Return_Id()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                CreatedBy = "testuser",
                ProduceDate = DateTime.Now,
                ManufactureEmail = "test@manufacture.com",
                ManufacturePhone = "123456789",
                IsAvailable = true
            };

            _productRepositoryMock
                .Setup(repo => repo.AddProductAsync(It.IsAny<Product>()))
                .Returns((Product product) =>
                {
                    product.Id = 1;
                    return Task.FromResult(product);
                });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(1, result);
            _productRepositoryMock.Verify(repo => repo.AddProductAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
