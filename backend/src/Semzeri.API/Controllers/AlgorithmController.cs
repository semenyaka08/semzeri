using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semzeri.BusinessLogic.DTOs.Algorithm;
using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL;

namespace Semzeri.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlgorithmController(IAlgorithmService algorithmService) : ControllerBase
{
    [Authorize(Roles = ApplicationRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> UpdateAlgorithm([FromBody] UpdateAlgorithmRequest request)
    {
        await algorithmService.UpdateAlgorithm(request);
        
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAlgorithm()
    {
        var algorithm = await algorithmService.GetAlgorithm();

        return Ok(algorithm);
    }
}