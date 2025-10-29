using Projeto.Domain;

namespace Projeto.Service;

public interface IProdutoService
{
    Task<Produto> CadastrarAsync(Produto produto);
    Task<IEnumerable<Produto>> AbaixoEstoqueMinimoAsync();
}
