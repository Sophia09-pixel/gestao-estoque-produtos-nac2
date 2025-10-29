SISTEMA DE GESTÃO DE ESTOQUE - README

=====================================================
DESCRIÇÃO DO PROJETO
=====================================================
Este projeto implementa um sistema de gestão de estoque para produtos perecíveis e não perecíveis. Permite controle de entrada e saída de mercadorias, controle de lotes e validade, e geração de relatórios sobre o estoque.

Tecnologias utilizadas:
- .NET 7 (API RESTful)
- MySQL (banco de dados)
- Swagger (para testes de endpoints)

-----------------------------------------------------
REGRAS DE NEGÓCIO IMPLEMENTADAS
-----------------------------------------------------
1. Produto
   - Cada produto possui SKU (identificador único), nome, categoria, preço unitário, quantidade mínima em estoque e data de criação.
   - Produtos perecíveis devem obrigatoriamente ter lote e data de validade.

2. Movimentação de Estoque
   - Movimentações podem ser do tipo ENTRADA ou SAÍDA.
   - Quantidade não pode ser negativa.
   - Saída só é permitida se houver estoque suficiente.
   - Produtos perecíveis não podem ter movimentações após a data de validade.

3. Alertas e Relatórios
   - Produtos com estoque abaixo da quantidade mínima são identificados.
   - Consulta de produtos que vencerão em até 7 dias.
   - Cálculo do valor total do estoque (quantidade × preço unitário).

-----------------------------------------------------
DIAGRAMA DE ENTIDADES (TEXTO)
-----------------------------------------------------
Produto
---------
SKU (PK)
Nome
Categoria (PERECIVEL / NAO_PERECIVEL)
PrecoUnitario
QuantidadeMinima
QuantidadeAtual
DataCriacao

MovimentacaoEstoque
------------------
Id (PK)
ProdutoSKU (FK -> Produto.SKU)
Tipo (ENTRADA / SAIDA)
Quantidade
DataMovimentacao
Lote (obrigatório para perecíveis)
DataValidade (obrigatório para perecíveis)

-----------------------------------------------------
EXEMPLOS DE REQUISIÇÕES API
-----------------------------------------------------

1. Cadastro de Produto
POST /api/produto
{
  "sku": "12345",
  "nome": "Leite Integral",
  "categoria": "PERECIVEL",
  "precoUnitario": 6.50,
  "quantidadeMinima": 10
}

2. Registrar Movimentação de Entrada
POST /api/movimentacao
{
  "produtoSKU": "12345",
  "tipo": "ENTRADA",
  "quantidade": 50,
  "lote": "L001",
  "dataValidade": "2025-11-10",
  "dataMovimentacao": "2025-10-29"
}

3. Registrar Movimentação de Saída
POST /api/movimentacao
{
  "produtoSKU": "12345",
  "tipo": "SAIDA",
  "quantidade": 20,
  "dataMovimentacao": "2025-10-29"
}

4. Relatórios
- Valor total do estoque: GET /api/relatorio/valor-total
- Produtos abaixo do mínimo: GET /api/relatorio/abaixo-minimo
- Produtos perecíveis vencendo em até 7 dias: GET /api/relatorio/vencendo-7dias

-----------------------------------------------------
COMO EXECUTAR O PROJETO
-----------------------------------------------------

1. Clone o repositório:
   git clone <URL_DO_REPOSITORIO>
   cd nome-do-projeto

2. Configure a conexão com o banco no appsettings.json:
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=fiap;User=root;Password=1234;Port=3306;"
   }

3. Crie o banco de dados `fiap` no MySQL.

4. Execute as migrações (se usar EF Core):
   dotnet ef database update

5. Execute a API:
   dotnet run

6. Acesse Swagger para testar os endpoints:
   https://localhost:5001/swagger
