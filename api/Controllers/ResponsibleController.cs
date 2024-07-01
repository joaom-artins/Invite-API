using api.Entities.Request;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("v1/responsible")]
    public class ResponsibleController(
        IResponsibleService _responsibleService
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _responsibleService.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _responsibleService.GetById(id);
            if (result is null)
            {
                return NotFound("Corno não encontrado");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResponsibleCreateRequest request)
        {
            var valid = await _responsibleService.CreateAsync(request);
            if (!valid)
            {
                return BadRequest("Corno não deu certo!!");
            }

            return NoContent();
        }
    }
}