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
    public class DeleteProductHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly DeleteProductHandler _handler;

        public DeleteProductHandlerTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new DeleteProductHandler(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Delete_Product()
        {
            // Arrange
            var command = new DeleteProductCommand
            {
                Id = 1,
                CreatedBy = "testuser"
            };

            var product = new Product
            {
                Id = 1,
                Name = "Test Product",
                CreatedBy = "testuser"
            };

            _productRepositoryMock
                .Setup(repo => repo.GetProductByIdAsync(command.Id))
                .ReturnsAsync(product);

            _productRepositoryMock
                .Setup(repo => repo.DeleteProductAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetProductByIdAsync(command.Id), Times.Once);
            _productRepositoryMock.Verify(repo => repo.DeleteProductAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
