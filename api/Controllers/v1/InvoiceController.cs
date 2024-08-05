using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/invoices")]
public class InvoiceController(
    IInvoiceService _invoiceService
) : ControllerBase
{
    [HttpPost("pay/{id}")]
    public async Task<IActionResult> Pay([FromRoute] Guid id, [FromBody] InvoicePayRequest request)
    {
        await _invoiceService.PayAsync(id, request);

        return NoContent();
    }
}