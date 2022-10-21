using Catalog.Api.Entities;
using Catalog.Api.Respositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository productRepository;

        public CatalogController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        //Post
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await productRepository.CreateProduct(product);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
            => Ok(await productRepository.GetProducts());

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Product>> GetproductById(string id) 
        {
            var product = await productRepository.Getproduct(id);

            if (product is null) return NotFound();

            return Ok(product);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> UpdateProduct([FromBody] Product product, string id) 
        {
            var dbProduct = await productRepository.Getproduct(id);

            if (dbProduct is null) return BadRequest();

            product.Id = id;
            await productRepository.Updateproduct(product);
            return Ok();

        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult> DeleteProduct(string id)
        {

            var dbProduct = await productRepository.Getproduct(id);

            if (dbProduct is null) return BadRequest();

            await productRepository.Deleteproduct(id);
            return Ok();

        }     

    }
}
