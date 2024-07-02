using Billing.Management.Application.Billing.DTOs;
using Billing.Management.Application.Billing.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Management.Api.Controllers.Billing
{
    [Authorize]
    [ApiController]
    [Route("api/billing")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingAppService? _service;

        public BillingController(IBillingAppService? service)
        {
            _service = service;
        }

        /// <summary>
        /// Get all billings paged.
        /// </summary>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <returns>A 200 code, with a list of billings, in case of success</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BillingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int pagenumber, int pagesize)
            => Ok(await _service?.GetAllAsync(pagenumber, pagesize));

        /// <summary>
        /// Get a billing.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A 200 code, with a billing, in case of success</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BillingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(Guid id)
            => Ok(await _service?.GetAsync(id));

        /// <summary>
        /// Creates a new billing inserting him/her in the data-base.
        /// </summary>
        /// <param name="billing"></param>
        /// <returns>A 201 code, in case of success</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(BillingDTO billing)
        {
            if (billing is BillingDTO)
            {
                await _service.CreateAsync(billing);

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
        /// Creates new billings inserting them in the data-base through an external API.
        /// </summary>
        /// <returns>A 201 code, in case of success</returns>
        [HttpPost("from-request")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync()
        {
            await _service.CreateFromRequestAsync();

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

        /// <summary>
        /// Updates a billing information.
        /// </summary>
        /// <param name="billing"></param>
        /// <returns>A 204 code, in case of success</returns>
        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(BillingDTO billing)
        {
            if (billing is BillingDTO { Id: Guid })
            {
                await _service.UpdateAsync(billing);
                return NoContent();
            }

            return BadRequest();
        }

        /// <summary>
        /// Delete a billing register.
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
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
