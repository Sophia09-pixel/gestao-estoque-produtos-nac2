using Dapper;
using MySqlConnector;
using Projeto.Domain;
using Microsoft.Extensions.Configuration;

namespace Projeto.Repository;

public class MovimentacaoRepository : IMovimentacaoRepository
{
    private readonly IConfiguration _config;
    public MovimentacaoRepository(IConfiguration config) => _config = config;
    private MySqlConnection Conexao() => new(_config.GetConnectionString("DefaultConnection"));

    public async Task InserirAsync(MovimentacaoEstoque mov)
    {
        using var conn = Conexao();
        var sql = @"INSERT INTO Movimentacoes 
                    (Tipo, Quantidade, DataMovimentacao, Lote, DataValidade, ProdutoSKU)
                    VALUES (@Tipo, @Quantidade, @DataMovimentacao, @Lote, @DataValidade, @ProdutoSKU)";
        await conn.ExecuteAsync(sql, mov);
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> ListarPorProdutoAsync(string sku)
    {
        using var conn = Conexao();
        var sql = "SELECT * FROM Movimentacoes WHERE ProdutoSKU = @sku";
        return await conn.QueryAsync<MovimentacaoEstoque>(sql, new { sku });
    }
}
