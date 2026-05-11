using IPO.Company.Interfaces;
using IPO.Company.Models.API;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IPO.Company.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyManagementService _companyManagementService;

        public CompanyController(ICompanyManagementService companyManagementService)
        {
            this._companyManagementService = companyManagementService;
        }

        [SwaggerOperation(Summary = "Retrieve company information.",
                          Description = "**Notes:** \n\n Retrieve company information, from Companies House, by company number.")]
        [Produces("application/json")]
        [HttpGet("/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CompanyDetailsResult>> GetCompany([FromQuery]CompanyDetailsRequest model)
        {
            var results = await this._companyManagementService.GetCompanyAddress(model);

            return Ok(results);
        }
    }
}
