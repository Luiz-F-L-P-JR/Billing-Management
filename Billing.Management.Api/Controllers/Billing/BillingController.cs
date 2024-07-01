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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BillingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(int pagenumber, int pagesize)
            => Ok(await _service?.GetAllAsync(pagenumber, pagesize));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BillingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(Guid id)
            => Ok(await _service?.GetAsync(id));

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

        [HttpPost("external-request")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync()
        {
            await _service.CreateAsync();

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
