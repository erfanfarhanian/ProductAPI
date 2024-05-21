using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Application.Products.Commands;
using ProductAPI.Application.Products.Queries;
using ProductAPI.Core.Entities;
using ProductAPI.Infrastructure.Data;
using System.Security.Claims;

namespace ProductAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator, ApplicationDbContext context)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? createdBy)
        {
            var query = new GetProductsQuery
            {
                CreatedBy = createdBy
            };
            var product = await _mediator.Send(query);

            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var query = new GetProductByIdQuery
            {
                Id = id
            };

            var product = await _mediator.Send(query);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            //command.CreatedBy = User.Identity.Name;
            //var productId = await _mediator.Send(command);

            //return CreatedAtAction(nameof(GetProduct), new { id = productId }, productId);

            var userNameClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userNameClaim))
            {
                return BadRequest("User is not authenticated");
            }

            command.CreatedBy = userNameClaim;
            var productId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetProduct), new { id = productId }, productId);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            //if (id != command.Id)
            //{
            //    return BadRequest("You do not have permission to update this product.");
            //}

            var userNameClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userNameClaim))
            {
                return BadRequest("User is not authenticated");
            }

            command.Id = id;
            command.CreatedBy = userNameClaim;
            await _mediator.Send(command);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var userNameClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            if (string.IsNullOrEmpty(userNameClaim))
            {
                return BadRequest("User is not authenticated");
            }

            var command = new DeleteProductCommand
            {
                Id = id,
                CreatedBy = userNameClaim
            };
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
