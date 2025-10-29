using Projeto.Domain;
using Projeto.Repository;

namespace Projeto.Service;

public class RelatorioService : IRelatorioService
{
    private readonly IProdutoRepository _produtoRepo;
    private readonly IMovimentacaoRepository _movRepo;

    public RelatorioService(IProdutoRepository produtoRepo, IMovimentacaoRepository movRepo)
    {
        _produtoRepo = produtoRepo;
        _movRepo = movRepo;
    }

    public async Task<decimal> ValorTotalEstoqueAsync()
    {
        var produtos = await _produtoRepo.ListarAsync();
        return produtos.Sum(p => p.PrecoUnitario * p.QuantidadeAtual);
    }

    public async Task<IEnumerable<Produto>> ProdutosAbaixoMinimoAsync()
    {
        var produtos = await _produtoRepo.ListarAsync();
        return produtos.Where(p => p.QuantidadeAtual < p.QuantidadeMinima);
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> ProdutosVencendoEm7DiasAsync()
    {
        var todos = new List<MovimentacaoEstoque>();
        var produtos = await _produtoRepo.ListarAsync();

        foreach (var p in produtos)
        {
            var movs = await _movRepo.ListarPorProdutoAsync(p.SKU);
            todos.AddRange(movs.Where(m =>
                m.DataValidade.HasValue &&
                m.DataValidade.Value <= DateTime.UtcNow.AddDays(7) &&
                m.DataValidade.Value >= DateTime.UtcNow));
        }

        return todos;
    }
}
