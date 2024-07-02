using Billing.Management.Application.Customer.DTO;
using Billing.Management.Application.Customer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace customer.Management.Api.Controllers.Customer
{
    [Authorize]
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerAppService? _service;

        public CustomerController(ICustomerAppService? service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all customers paged.
        /// </summary>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <returns>A 200 code, with a list of customers, in case of success</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int pagenumber, int pagesize)
            => Ok(await _service?.GetAllAsync(pagenumber, pagesize));

        /// <summary>
        /// Get a customer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A 200 code, with a customer, in case of success</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(CustomerDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(Guid id)
            => Ok(await _service?.GetAsync(id));

        /// <summary>
        /// Creates a new customer inserting him/her in the data-base.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>A 201 code, in case of success</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(CustomerDTO customer)
        {
            if(customer is CustomerDTO)
            {
                await _service?.CreateAsync(customer);
                
                return Created
                (
                    "Created", 
                    new 
                    {
                        ResponseCode = StatusCodes.Status201Created,
                        ResponseMessage = "Customer successfully created."                        
                    }
                );
            }

            return BadRequest();
        }

        /// <summary>
        /// Updates a customer information.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>A 204 code, in case of success</returns>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(CustomerDTO customer)
        {
            if (customer is CustomerDTO { Id: Guid })
            {
                await _service?.UpdateAsync(customer);
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete a customer register.
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
