using Billing.Management.Application.Product.DTO;
using Billing.Management.Application.Product.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductAppService? _service;

        public ProductController(IProductAppService? service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int pagenumber, int pagesize)
            => Ok(await _service.GetAllAsync(pagenumber, pagesize));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProductDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _service.GetAsync(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(ProductDTO product)
        {
            if(product is ProductDTO)
            {
                await _service.CreateAsync(product);
                return Created();
            }

            return BadRequest();
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(ProductDTO product)
        {
            if (product is ProductDTO { Id: Guid })
            {
                await _service.UpdateAsync(product);
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
