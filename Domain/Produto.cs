namespace Projeto.Domain;

public enum CategoriaProduto
{
    PERECIVEL,
    NAO_PERECIVEL
}

public class Produto
{
    public string SKU { get; set; } = Guid.NewGuid().ToString();
    public string Nome { get; set; }
    public CategoriaProduto Categoria { get; set; }
    public decimal PrecoUnitario { get; set; }
    public int QuantidadeMinima { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public int QuantidadeAtual { get; set; } = 0;
}
