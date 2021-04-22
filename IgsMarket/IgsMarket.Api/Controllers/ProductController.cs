using IgsMarket.Api.Http;
using IgsMarket.Api.Model;
using IgsMarket.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IgsMarket.Api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: <ProductController>
        [HttpGet("/v{version:apiVersion}/Products")]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productRepository.GetAllProducts();
        }

        // GET <ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productRepository.GetProduct(id);

            return product == null
                ? NotFound()
                : Ok(product);
        }

        // POST <ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CreateProductRequest request)
        {
            await _productRepository.CreateProduct(new Product(request.Name, request.Price));
            return Ok();
        }

        // PUT <ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] UpdateProductRequest request)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                product.SetName(request.Name);
            }

            if (request.Price.HasValue)
            {
                product.SetPrice(request.Price.Value);
            }

            await _productRepository.UpdateProduct(product);

            return Ok();
        }

        // DELETE <ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.DeleteProduct(product);

            return Ok();
        }
    }
}
