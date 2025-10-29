using Projeto.Domain;

namespace Projeto.Service;

public interface IMovimentacaoService
{
    Task<MovimentacaoEstoque> RegistrarAsync(MovimentacaoEstoque mov);
}
