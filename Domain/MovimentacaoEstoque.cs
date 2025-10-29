namespace Projeto.Domain;

public enum TipoMovimentacao
{
    ENTRADA,
    SAIDA
}

public class MovimentacaoEstoque
{
    public int Id { get; set; }
    public TipoMovimentacao Tipo { get; set; }
    public int Quantidade { get; set; }
    public DateTime DataMovimentacao { get; set; } = DateTime.UtcNow;
    public string? Lote { get; set; }
    public DateTime? DataValidade { get; set; }
    public string ProdutoSKU { get; set; }
}
