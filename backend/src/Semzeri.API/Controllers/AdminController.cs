using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Semzeri.BusinessLogic.DTOs.Admin;
using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL;

namespace Semzeri.API.Controllers;

public class AdminController (IUrlsService urlsService) : ControllerBase
{
    [HttpGet("urls")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> GetAllUrls([FromQuery] AdminUrlsGetRequest request)
    {
        var urls = await urlsService.GetAllUrlsAsync(request);

        return Ok(urls);
    }
}