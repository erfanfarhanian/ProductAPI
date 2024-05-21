using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductAPI.Application.Products.Commands;
using ProductAPI.Application.Products.Queries;
using ProductAPI.Core.Entities;
using ProductAPI.Infrastructure.Data;
using ProductAPI.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductController _controller;
        private readonly ApplicationDbContext _context;

        public ProductControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);

            _controller = new ProductController(_mediatorMock.Object, _context);
        }

        [Fact]
        public async Task GetProducts_Should_Return_Products()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1", CreatedBy = "user1" },
                new Product { Id = 2, Name = "Product 2", CreatedBy = "user2" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
                .ReturnsAsync(products);

            // Act
            var result = await _controller.GetProducts(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(2, returnProducts.Count);
        }

        [Fact]
        public async Task CreateProduct_Should_Call_Mediator_And_Return_CreatedAtActionResult()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "New Product",
                CreatedBy = "user1",
                ProduceDate = DateTime.Now,
                ManufactureEmail = "test@manufacture.com",
                ManufacturePhone = "123456789",
                IsAvailable = true
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default))
                .ReturnsAsync(1);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "user1")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };

            // Act
            var result = await _controller.CreateProduct(command);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(1, createdAtActionResult.RouteValues["id"]);
        }
    }
}
