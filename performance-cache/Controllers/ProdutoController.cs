using Microsoft.AspNetCore.Mvc;
using Projeto.Domain;
using Projeto.Service;

namespace Projeto.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _service;
    public ProdutoController(IProdutoService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Produto produto)
    {
        var result = await _service.CadastrarAsync(produto);
        return CreatedAtAction(nameof(GetBySku), new { sku = result.SKU }, result);
    }

    [HttpGet("{sku}")]
    public async Task<IActionResult> GetBySku(string sku)
    {
        return Ok($"Buscar produto {sku}");
    }

    [HttpGet("baixo-estoque")]
    public async Task<IActionResult> AbaixoEstoque()
    {
        var produtos = await _service.AbaixoEstoqueMinimoAsync();
        return Ok(produtos);
    }
}
