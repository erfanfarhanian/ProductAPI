using AutoMapper;
using Moq;
using ProductAPI.Application.Products.Commands;
using ProductAPI.Application.Products.Handlers;
using ProductAPI.Core.Entities;
using ProductAPI.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAPI.Tests.Handlers
{
    public class UpdateProductHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly IMapper _mapper;
        private readonly UpdateProductHandler _handler;

        public UpdateProductHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateProductCommand, Product>();
            });

            _mapper = config.CreateMapper();
            _handler = new UpdateProductHandler(_productRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task Handle_Should_Update_Product()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                Id = 1,
                Name = "Updated Product",
                CreatedBy = "testuser",
                ProduceDate = DateTime.Now,
                ManufactureEmail = "updated@manufacture.com",
                ManufacturePhone = "987654321",
                IsAvailable = false
            };

            var existingProduct = new Product
            {
                Id = 1,
                Name = "Test Product",
                CreatedBy = "testuser",
                ProduceDate = DateTime.Now,
                ManufactureEmail = "test@manufacture.com",
                ManufacturePhone = "123456789",
                IsAvailable = true
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductByIdAsync(command.Id))
                .ReturnsAsync(existingProduct);

            _productRepositoryMock
                .Setup(repo => repo.UpdateProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetProductByIdAsync(command.Id), Times.Once);
            _productRepositoryMock.Verify(repo => repo.UpdateProductAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
