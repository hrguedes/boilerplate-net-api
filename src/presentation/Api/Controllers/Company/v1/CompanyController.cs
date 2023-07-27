using Application.Company.Interfaces;
using Base;
using Dto.Company.Request;
using Dto.Company.Response;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Company.v1;



[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CompanyController : ControllerBase
{
    /// <summary>
    /// Add new Company
    /// </summary>
    /// <param name="company"></param>
    /// <param name="request"></param>
    /// <remarks>
    /// POST /api/v1/Company/add
    /// </remarks>
    /// <returns> New Company Created </returns>
    /// <response code="200"> Company Created</response>
    /// <response code="400">Client Error</response>
    /// <response code="500">Server Error (Contact Admin)</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(ReturnOk<List<CompanyResponse>>), 200)]
    [ProducesResponseType(typeof(ReturnOk<List<CompanyResponse>>), 400)]
    [ProducesResponseType(typeof(string), 500)]
    public async Task<IActionResult> AddNewCompany(
        [FromServices] ICompanyService company,
        [FromBody] CreateNewCompanyRequest request
    )
    {
        try
        {
            var resp = await company.CreateNewCompany(request);
            return StatusCode(resp.StatusCode, resp);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}