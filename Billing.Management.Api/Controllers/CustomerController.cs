using Billing.Management.Application.Customer.DTO;
using Billing.Management.Application.Customer.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace customer.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService? _service;

        public CustomerController(ICustomerAppService? service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int pagenumber, int pagesize)
            => Ok(await _service.GetAllAsync(pagenumber, pagesize));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _service.GetAsync(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CustomerDTO customer)
        {
            if(customer is CustomerDTO)
            {
                await _service.CreateAsync(customer);
                return Created();
            }

            return BadRequest();
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(CustomerDTO customer)
        {
            if (customer is CustomerDTO { Id: Guid })
            {
                await _service.UpdateAsync(customer);
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
