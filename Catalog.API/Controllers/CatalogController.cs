using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Catalog.API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productRepository.GetAll();

            if (products is null || !products.Any())
                return NotFound();

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ValidateIdLength("CatalogSettings:IdLength")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetById(string id)
        {
            var products = await _productRepository.GetById(id);

            if (products is null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("[action]/{category}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var products = await _productRepository.GetProductByCategory(category);

            if (products is null)
                return NotFound();

            return Ok(products);
        }

        [HttpGet("[action]/{name}")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            var products = await _productRepository.GetProductByName(name);

            if (products is null)
                return NotFound();

            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            if (product == null) return BadRequest("Invalid product");

            await _productRepository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id });
        }

        [HttpDelete("{id}")]
        [ValidateIdLength("CatalogSettings:IdLength")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> Delete(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }

        [HttpPut]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> Update([FromBody] Product product)
        {
            if (product == null) return BadRequest("Invalid product");

            await _productRepository.Update(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id });
        }
    }
}
