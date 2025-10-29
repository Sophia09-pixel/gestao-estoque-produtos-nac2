using Projeto.Domain;
using Projeto.Repository;

namespace Projeto.Service;

public class MovimentacaoService : IMovimentacaoService
{
    private readonly IProdutoRepository _produtoRepo;
    private readonly IMovimentacaoRepository _movRepo;

    public MovimentacaoService(IProdutoRepository produtoRepo, IMovimentacaoRepository movRepo)
    {
        _produtoRepo = produtoRepo;
        _movRepo = movRepo;
    }

    public async Task<MovimentacaoEstoque> RegistrarAsync(MovimentacaoEstoque mov)
    {
        if (mov.Quantidade <= 0)
            throw new Exception("A quantidade deve ser positiva.");

        var produto = await _produtoRepo.BuscarPorSkuAsync(mov.ProdutoSKU)
            ?? throw new Exception("Produto não encontrado.");

        if (produto.Categoria == CategoriaProduto.PERECIVEL)
        {
            if (string.IsNullOrWhiteSpace(mov.Lote) || mov.DataValidade == null)
                throw new Exception("Produtos perecíveis exigem lote e data de validade.");

            if (mov.DataValidade < DateTime.UtcNow)
                throw new Exception("Não é possível movimentar produtos vencidos.");
        }

        if (mov.Tipo == TipoMovimentacao.SAIDA)
        {
            if (produto.QuantidadeAtual < mov.Quantidade)
                throw new Exception("Estoque insuficiente para saída.");

            produto.QuantidadeAtual -= mov.Quantidade;
        }
        else
        {
            produto.QuantidadeAtual += mov.Quantidade;
        }

        await _produtoRepo.AtualizarQuantidadeAsync(produto.SKU, produto.QuantidadeAtual);
        await _movRepo.InserirAsync(mov);

        return mov;
    }
}
