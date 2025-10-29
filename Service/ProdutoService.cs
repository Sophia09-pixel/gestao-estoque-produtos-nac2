using Projeto.Domain;
using Projeto.Repository;

namespace Projeto.Service;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repo;
    public ProdutoService(IProdutoRepository repo) => _repo = repo;

    public async Task<Produto> CadastrarAsync(Produto produto)
    {
        if (produto.Categoria == CategoriaProduto.PERECIVEL && produto.QuantidadeMinima <= 0)
            throw new Exception("Produto perecível deve ter quantidade mínima válida.");

        await _repo.InserirAsync(produto);
        return produto;
    }

    public async Task<IEnumerable<Produto>> AbaixoEstoqueMinimoAsync()
    {
        var produtos = await _repo.ListarAsync();
        return produtos.Where(p => p.QuantidadeAtual < p.QuantidadeMinima);
    }
}
