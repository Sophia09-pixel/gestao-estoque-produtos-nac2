using Projeto.Domain;

namespace Projeto.Service;

public interface IRelatorioService
{
    Task<decimal> ValorTotalEstoqueAsync();
    Task<IEnumerable<Produto>> ProdutosAbaixoMinimoAsync();
    Task<IEnumerable<MovimentacaoEstoque>> ProdutosVencendoEm7DiasAsync();
}
