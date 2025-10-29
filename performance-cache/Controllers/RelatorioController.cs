using Microsoft.AspNetCore.Mvc;
using Projeto.Service;

namespace Projeto.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly IRelatorioService _service;

    public RelatorioController(IRelatorioService service)
    {
        _service = service;
    }

    [HttpGet("valor-total")]
    public async Task<IActionResult> ValorTotal()
    {
        var valor = await _service.ValorTotalEstoqueAsync();
        return Ok(new { valorTotal = valor });
    }

    [HttpGet("abaixo-minimo")]
    public async Task<IActionResult> AbaixoMinimo()
    {
        var produtos = await _service.ProdutosAbaixoMinimoAsync();
        return Ok(produtos);
    }

    [HttpGet("vencendo-7dias")]
    public async Task<IActionResult> Vencendo7Dias()
    {
        var vencendo = await _service.ProdutosVencendoEm7DiasAsync();
        return Ok(vencendo);
    }
}
