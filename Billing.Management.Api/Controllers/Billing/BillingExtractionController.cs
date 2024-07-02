using Billing.Management.Application.Billing.DTOs;
using Billing.Management.Application.Billing.Services.Interfaces;
using Billing.Management.Application.FileHandler.Excel.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Management.Api.Controllers.Billing
{
    [Authorize]
    [ApiController]
    [Route("api/billing-extraction")]
    public class BillingExtractionController : ControllerBase
    {
        private readonly IExcelBuilder<BillingDTO>? _fileBuilder;
        private readonly IBillingAppService? _service;

        public BillingExtractionController
        (
            IExcelBuilder<BillingDTO>? fileBuilder, 
            IBillingAppService? service
        )
        {
            _fileBuilder = fileBuilder;
            _service = service;
        }

        /// <summary>
        /// Extracts the billing informations paged and generates an excel file.
        /// </summary>
        /// <param name="pagenumber"></param>
        /// <param name="pagesize"></param>
        /// <returns>A 200 code, with a link to download an excel file containing a list of billings, in case of success</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(BillingDTO), StatusCodes.Status200OK)]
        public async Task<FileResult> ExtractionAsync(int pagenumber, int pagesize)
        {
            var billings = await _service.GetAllWithLinesAsync(pagenumber, pagesize);
            string fileType = @"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8";
            var fileStream = _fileBuilder.CreateFile(billings.ToList());

            return File(fileStream, fileType, "Billings.xlsx");
        }
    }
}
