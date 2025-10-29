using Projeto.Domain;

namespace Projeto.Repository;

public interface IMovimentacaoRepository
{
    Task InserirAsync(MovimentacaoEstoque mov);
    Task<IEnumerable<MovimentacaoEstoque>> ListarPorProdutoAsync(string sku);
}
