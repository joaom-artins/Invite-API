using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/system")]
public class SystemController(
    ISystemService _systemService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "System")]
    public async Task<IActionResult> AutomatedCreateInvoice()
    {
        await _systemService.AutomatedInvoiceCreateasync();

        return NoContent();
    }
}
