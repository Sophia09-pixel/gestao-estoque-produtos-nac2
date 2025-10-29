using Microsoft.AspNetCore.Mvc;
using Projeto.Domain;
using Projeto.Service;

namespace Projeto.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimentacaoController : ControllerBase
{
    private readonly IMovimentacaoService _service;

    public MovimentacaoController(IMovimentacaoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MovimentacaoEstoque mov)
    {
        try
        {
            var result = await _service.RegistrarAsync(mov);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }
}
