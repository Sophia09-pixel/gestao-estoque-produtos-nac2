using Dapper;
using MySqlConnector;
using Projeto.Domain;
using Microsoft.Extensions.Configuration;

namespace Projeto.Repository;

public class ProdutoRepository : IProdutoRepository
{
    private readonly IConfiguration _config;
    public ProdutoRepository(IConfiguration config) => _config = config;

    private MySqlConnection Conexao() => new(_config.GetConnectionString("DefaultConnection"));

    public async Task<IEnumerable<Produto>> ListarAsync()
    {
        using var conn = Conexao();
        return await conn.QueryAsync<Produto>("SELECT * FROM Produtos");
    }

    public async Task<Produto?> BuscarPorSkuAsync(string sku)
    {
        using var conn = Conexao();
        return await conn.QueryFirstOrDefaultAsync<Produto>(
            "SELECT * FROM Produtos WHERE SKU = @sku", new { sku });
    }

    public async Task InserirAsync(Produto produto)
    {
        using var conn = Conexao();
        var sql = @"INSERT INTO Produtos 
                    (SKU, Nome, Categoria, PrecoUnitario, QuantidadeMinima, DataCriacao, QuantidadeAtual)
                    VALUES (@SKU, @Nome, @Categoria, @PrecoUnitario, @QuantidadeMinima, @DataCriacao, @QuantidadeAtual)";
        await conn.ExecuteAsync(sql, produto);
    }

    public async Task AtualizarQuantidadeAsync(string sku, int novaQuantidade)
    {
        using var conn = Conexao();
        await conn.ExecuteAsync("UPDATE Produtos SET QuantidadeAtual = @novaQuantidade WHERE SKU = @sku",
            new { novaQuantidade, sku });
    }
}
