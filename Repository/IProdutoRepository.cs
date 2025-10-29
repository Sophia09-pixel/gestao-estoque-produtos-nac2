using Projeto.Domain;

namespace Projeto.Repository;

public interface IProdutoRepository
{
    Task<IEnumerable<Produto>> ListarAsync();
    Task<Produto?> BuscarPorSkuAsync(string sku);
    Task InserirAsync(Produto produto);
    Task AtualizarQuantidadeAsync(string sku, int novaQuantidade);
}
