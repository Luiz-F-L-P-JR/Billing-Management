using Billing.Management.Application.Product.DTO;
using Billing.Management.Application.Product.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Management.Api.Controllers.Product
{
    [Authorize]
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService? _service;

        public ProductController(IProductAppService? service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all products paged.
        /// </summary>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <returns>A 200 code, with a list of products, in case of success</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int pagenumber, int pagesize)
            => Ok(await _service?.GetAllAsync(pagenumber, pagesize));

        /// <summary>
        /// Get a product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A 200 code, with a product, in case of success</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(Guid id)
            => Ok(await _service?.GetAsync(id));

        /// <summary>
        /// Creates a new product inserting him/her in the data-base.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A 201 code, in case of success</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(ProductDTO product)
        {
            if(product is ProductDTO)
            {
                await _service?.CreateAsync(product);

                return Created
                (
                    "Created", 
                    new 
                    {
                        ResponseCode = StatusCodes.Status201Created,
                        ResponseMessage = "Product successfully created."                        
                    }
                );
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates a product information.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>A 204 code, in case of success</returns>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(ProductDTO product)
        {
            if (product is ProductDTO { Id: Guid })
            {
                await _service?.UpdateAsync(product);
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete a product register.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A 204 code, in case of success</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _service?.DeleteAsync(id);
            return NoContent();
        }
    }
}
