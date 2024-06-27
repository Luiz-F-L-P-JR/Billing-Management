using Billing.Management.Application.Billing.DTOs;
using Billing.Management.Application.Billing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Management.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingAppService? _service;

        public BillingController(IBillingAppService? service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BillingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BillingDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _service.GetAsync(id));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(BillingDTO billing)
        {
            if (billing is BillingDTO)
            {
                await _service.CreateAsync(billing);
                return Created();
            }

            return BadRequest();
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(BillingDTO billing)
        {
            if (billing is BillingDTO { Id: Guid })
            {
                await _service.UpdateAsync(billing);
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
