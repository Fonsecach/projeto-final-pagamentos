using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//cadastro de pessoas
app.MapPost("/api/pessoas/cadastrar", async ([FromBody] List<Pessoa> pessoas, [FromServices] AppDataContext contextPessoas) =>
{
    var pessoasNaoSalvas = new List<Pessoa>();

    foreach (var pessoa in pessoas)
    {
        // Verifica se a pessoa já está na base de dados pelo ID ou NumDocumento
        var pessoaExistente = await contextPessoas.Pessoas.FirstOrDefaultAsync(p => p.ID == pessoa.ID || p.NumDocumento == pessoa.NumDocumento);

        if (pessoaExistente != null)
        {
            // Pessoa já está na base de dados, então retorna erro
            return Results.Conflict($"A pessoa com ID {pessoa.ID} ou NumDocumento {pessoa.NumDocumento} já está cadastrada.");
        }
        else
        {
            // Pessoa não está na base de dados, então salva
            contextPessoas.Pessoas.Add(pessoa);
            pessoasNaoSalvas.Add(pessoa);
        }
    }

    await contextPessoas.SaveChangesAsync();

    return Results.Created("", pessoasNaoSalvas);

}).WithName("AddPessoas").WithOpenApi();

//consulta de todas as pessoas
app.MapGet("/api/pessoas/exibir", async ([FromServices] AppDataContext contextPessoas) =>
{
    var pessoas = await contextPessoas.Pessoas.ToListAsync();
    if (pessoas.Any()){
        var pessoasOrdenadas = pessoas.OrderBy(p => p.Nome).ToList();
        return Results.Ok(pessoasOrdenadas);
    }
    return Results.NotFound("Nenhuma pessoa foi registrada");

}).WithName("ExibirPessoas").WithOpenApi();


//cadastro de pedidos

app.MapPost("/api/pedido/cadastrar", async ([FromBody] List<Pedido> pedidos, [FromServices] AppDataContext contextPedidos) =>
{
    var pedidoNaoSalvo = new List<Pedido>();

    foreach (var pedido in pedidos)
    {
        // Verifica se o pedido já está na base de dados pelo ID
        var pedidoExistente = await contextPedidos.Pedidos.FirstOrDefaultAsync(p => p.ID == pedido.ID);

        if (pedidoExistente != null)
        {
            // Pedido já está na base de dados, então retorna erro
            return Results.Conflict($"A pessoa com ID {pedido.ID} já está cadastrado.");
        }
        else
        {
            // Pessoa não está na base de dados, então salva
            contextPedidos.Pedidos.Add(pedido);
            pedidoNaoSalvo.Add(pedido);
        }
    }

    await contextPedidos.SaveChangesAsync();

    return Results.Created("", pedidoNaoSalvo);

}).WithName("cadastrarPedidos").WithOpenApi();


//consulta de todos os pedidos
app.MapGet("/api/pedido/exibir", async ([FromServices] AppDataContext contextPedidos) =>
{
    var pedidos = await contextPedidos.Pedidos.ToListAsync();
    if (pedidos.Any()){
        return Results.Ok(pedidos);
    }
    return Results.NotFound("Nenhuma pedido foi registrada");

}).WithName("ExibirPedidos").WithOpenApi();


app.Run();
