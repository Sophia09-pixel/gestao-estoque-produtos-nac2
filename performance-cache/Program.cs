using Projeto.Repository;
using Projeto.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IMovimentacaoService, MovimentacaoService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
