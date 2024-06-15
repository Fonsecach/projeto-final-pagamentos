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
builder.Services.AddCors(options => {
    options.AddPolicy("AcessoTotal",
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

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
app.MapGet("/api/pessoas/exibir", async ([FromServices] AppDataContext contextPessoas, int pageNumber = 1, int pageSize = 10) =>
{
    if (pageNumber <= 0 || pageSize <= 0)
    {
        return Results.BadRequest("O número da página e o tamanho da página devem ser maiores que zero.");
    }

    var totalPessoas = await contextPessoas.Pessoas.CountAsync();
    var totalPages = (int)Math.Ceiling(totalPessoas / (double)pageSize);

    var pessoas = await contextPessoas.Pessoas
        .Include(p => p.Enderecos)
        .Include(p => p.Contatos)
        .OrderBy(p => p.Nome)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    if (pessoas.Any())
    {
        var result = new
        {
            TotalPessoas = totalPessoas,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            Pessoas = pessoas
        };
        return Results.Ok(result);
    }

    return Results.NotFound("Nenhuma pessoa foi registrada");
}).WithName("ExibirPessoas").WithOpenApi();




//consulta  pessoa por ID
app.MapGet("/api/pessoas/exibir/id/{id}", async ([FromServices] AppDataContext contextPessoas, int id) =>
{
    var pessoa = await contextPessoas.Pessoas.
    Include(p => p.Enderecos).
    Include(p => p.Contatos).
    FirstOrDefaultAsync(p => p.ID == id);
    if (pessoa != null)
    {
        return Results.Ok(pessoa);
    }
    return Results.NotFound($"Pessoa com ID {id} não foi encontrada");

}).WithName("ExibirPessoaPorId").WithOpenApi();

//consultar pessoa por nome
app.MapGet("/api/pessoas/exibir/nome/{nome}", async ([FromServices] AppDataContext contextPessoas, string nome) =>
{
    var pessoas = await contextPessoas.Pessoas
        .Include(p => p.Enderecos)
        .Include(p => p.Contatos)
        .Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
        .ToListAsync();

    if (pessoas.Any())
    {
        var pessoasOrdenadas = pessoas.OrderBy(p => p.Nome).ToList();
        return Results.Ok(pessoasOrdenadas);
    }

    return Results.NotFound($"Pessoa com nome contendo '{nome}' não foi encontrada");
}).WithName("ExibirPessoaPorNome").WithOpenApi();


//alterar pessoa
app.MapPut("/api/pessoas/alterar/{id}", async ([FromRoute] int id, [FromBody] Pessoa pessoaAtualizada, [FromServices] AppDataContext contextPessoas) =>
{
    var pessoaExistente = await contextPessoas.Pessoas
        .Include(p => p.Enderecos)
        .Include(p => p.Contatos)
        .FirstOrDefaultAsync(p => p.ID == id);

    if (pessoaExistente is null)
    {
        return Results.NotFound("Pessoa não localizada");
    }

    // Atualizar as propriedades da pessoa existente com os dados da pessoaAtualizada
    pessoaExistente.Nome = pessoaAtualizada.Nome;
    pessoaExistente.NomeFantasia = pessoaAtualizada.NomeFantasia;
    pessoaExistente.NumDocumento = pessoaAtualizada.NumDocumento;
    pessoaExistente.Tipo = pessoaAtualizada.Tipo;
    pessoaExistente.AtualizadoEm = DateTime.Now;

    // Atualizar os endereços
    pessoaExistente.Enderecos.Clear();
    foreach (var endereco in pessoaAtualizada.Enderecos)
    {
        pessoaExistente.Enderecos.Add(endereco);
    }

    // Atualizar os contatos
    pessoaExistente.Contatos.Clear();
    foreach (var contato in pessoaAtualizada.Contatos)
    {
        pessoaExistente.Contatos.Add(contato);
    }

    await contextPessoas.SaveChangesAsync();

    return Results.Ok(pessoaExistente);
}).WithName("AtualizarPessoa").WithOpenApi();


//excluir pessoa

app.MapDelete("/api/pessoas/deletar/{id}", async ([FromRoute] int id, [FromServices] AppDataContext contextPessoas) =>
{
    var pessoa = await contextPessoas.Pessoas.FindAsync(id);
    if (pessoa is null)
    {
        return Results.NotFound("Pessoa não localizada");
    }
    contextPessoas.Pessoas.Remove(pessoa);
    await contextPessoas.SaveChangesAsync();

    return Results.Ok(pessoa);
}).WithName("DeletarPessoa").WithOpenApi();

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
            return Results.Conflict($"Um pedido com ID {pedido.ID} já está cadastrado.");
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
    if (pedidos.Any())
    {
        return Results.Ok(pedidos);
    }
    return Results.NotFound("Nenhuma pedido foi registrada");

}).WithName("ExibirPedidos").WithOpenApi();


//buscar pedido por ID
app.MapGet("/api/pedido/exibir/id/{id}", async ([FromServices] AppDataContext contextPedidos, int id) =>
{
    var pedido = await contextPedidos.Pedidos.FirstOrDefaultAsync(p => p.ID == id);
    if (pedido != null)
    {
        return Results.Ok(pedido);
    }
    return Results.NotFound($"Pedido com ID {id} não foi encontrada");

}).WithName("ExibirPedidoPorId").WithOpenApi();

//buscar pedido por nome
app.MapGet("/api/pedido/exibir/Descricao/{descricao}", async ([FromServices] AppDataContext contextPedidos, string descricao) =>
{
    var pedido = await contextPedidos.Pedidos.FirstOrDefaultAsync(p => p.Descricao == descricao);
    if (pedido != null)
    {
        return Results.Ok(pedido);
    }
    return Results.NotFound($"Pedido com descricao {descricao} não foi encontrada");

}).WithName("ExibirPedidoPorDescricao").WithOpenApi();

// Retorna os pedidos aguardando pagamento
app.MapGet("/api/pedidos/pagamento/pendente", async ([FromServices] AppDataContext contextPedidos) =>
{
    var pedidosAguardando = await contextPedidos.Pedidos
        .Where(p => p.Status == "Aguarda pagamento")
        .ToListAsync();

    if (pedidosAguardando == null || pedidosAguardando.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Json(pedidosAguardando);
}).WithName("ExibirPedidosAguardandoPagamento").WithOpenApi();

//Retorna os pedidos que não foram pagos até a data de vencimento
app.MapGet("/api/pedidos/pagamento/vencidos", async ([FromServices] AppDataContext contextPedidos) =>
{
    var pedidosVencidos = await contextPedidos.Pedidos
        .Where(p => p.Status == "Aguardando Pagamento" && p.DataDoVencimento < DateTime.Now)
        .ToListAsync();

    if (pedidosVencidos == null || pedidosVencidos.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Json(pedidosVencidos);
}).WithName("ExibirPedidosVencidos").WithOpenApi();

// Retorna os pedidos pagos
app.MapGet("/api/pedidos/pagamento/pago", async ([FromServices] AppDataContext contextPedidos) =>
{
    var pedidosPagos = await contextPedidos.Pedidos
        .Where(p => p.Status == "Pago")
        .ToListAsync();

    if (pedidosPagos == null || pedidosPagos.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Json(pedidosPagos);
}).WithName("ExibirPedidosPagos").WithOpenApi();

//alterar pedidos
app.MapPut("/api/pedido/alterar/{id}", async ([FromRoute] int id, [FromBody] Pedido pedidoAtualizado, [FromServices] AppDataContext contextPedidos) =>

{

    var pedidoExistente = await contextPedidos.Pedidos.FindAsync(id);

    if (pedidoExistente is null)

    {

        return Results.NotFound("Pedido não localizado");

    }

    // Atualizar as propriedades do pedido existente com os dados do pedidoAtualizado
    pedidoExistente.Descricao = pedidoAtualizado.Descricao;
    pedidoExistente.ValorTotal = pedidoAtualizado.ValorTotal;
    pedidoExistente.Descricao = pedidoAtualizado.Descricao;
    pedidoExistente.DataDoPedido = pedidoAtualizado.DataDoPedido;
    pedidoExistente.DataDoVencimento = pedidoAtualizado.DataDoVencimento;
    pedidoExistente.Status = pedidoAtualizado.Status;
    pedidoExistente.DevedorID = pedidoAtualizado.DevedorID;
    pedidoExistente.CredorID = pedidoAtualizado.CredorID;
    
    await contextPedidos.SaveChangesAsync();

    return Results.Ok(pedidoAtualizado);

}).WithName("PedidoAtualizado").WithOpenApi();

//deletar pedidos
app.MapDelete("/api/pedido/deletar/{id}", async ([FromRoute] int id, [FromServices] AppDataContext contextPedidos) =>
{
    var pedido = await contextPedidos.Pedidos.FindAsync(id);
    if (pedido is null)
    {
        return Results.NotFound("Pedido não localizada");
    }
    contextPedidos.Pedidos.Remove(pedido);
    await contextPedidos.SaveChangesAsync();

    return Results.Ok(pedido);
}).WithName("DeletarPedido").WithOpenApi();

//cadastro de pagamentos
app.MapPost("/api/pagamento/cadastrar", async ([FromBody] Pagamento pagamento, [FromServices] AppDataContext contextPagamentos) =>
{
    if (pagamento is null)
    {
        return Results.BadRequest("O pagamento enviado é inválido!");
    }

    // Verificação adicional para evitar pagamentos duplicados com o mesmo PedidoID
    var pagamentoExistenteComPedidoID = await contextPagamentos.Pagamentos
        .FirstOrDefaultAsync(p => p.PedidoID == pagamento.PedidoID);

    if (pagamentoExistenteComPedidoID != null)
    {
        return Results.BadRequest("Já existe um pagamento registrado para este PedidoID!");
    }

    // 1. Adicionar o pagamento ao banco de dados
    contextPagamentos.Pagamentos.Add(pagamento);
    await contextPagamentos.SaveChangesAsync();

    // 2. Buscar o pedido correspondente
    var pedido = await contextPagamentos.Pedidos.FindAsync(pagamento.PedidoID);
    if (pedido == null)
    {
        return Results.NotFound("Pedido não encontrado.");
    }

    // 3. Verificar se o pedido está aguardando pagamento
    if (pedido.Status == "Aguardando Pagamento")
    {
        // 4. Atualizar o status do pedido para "Pago"
        pedido.Status = "Pago";
        await contextPagamentos.SaveChangesAsync();
    }

    return Results.Created($"/api/pagamentos/{pagamento.ID}", pagamento);
}).WithName("CadastrarPagamentos").WithOpenApi();

//consulta de todos os pagamentos
app.MapGet("/api/pagamentos/exibir", async ([FromServices] AppDataContext contextPagamentos) =>
{
    var pagamentos = await contextPagamentos.Pagamentos.ToListAsync();
    if (pagamentos.Any())
    {
        return Results.Ok(pagamentos);
    }
    return Results.NotFound("Nenhuma pedido foi registrada");

}).WithName("ExibirPagamentos").WithOpenApi();

//Consulta de todos pagamentos recebidos por ID
app.MapGet("/api/pagamentos/recebidos/exibir/{id}", async ([FromServices] AppDataContext contextPagamentos, int id) =>
{
    var pagamentos = await contextPagamentos.Pagamentos
        .Where(p => p.CredorID == id)
        .ToListAsync();

    if (pagamentos == null || pagamentos.Count == 0)
    {
        return Results.NotFound();
    }

    return Results.Json(pagamentos);
}).WithName("ExibirPagamentosRecebidosPorId").WithOpenApi();

//Consulta a média de pagamentos recebidos por ID do banco de dados
app.MapGet("/api/pagamentos/recebidos/media/{id}", async ([FromServices] AppDataContext contextPagamentos, int id) =>
{
    var pagamentos = await contextPagamentos.Pagamentos
        .Where(p => p.CredorID == id)
        .Select(p => p.Valor)
        .ToListAsync();

    var mediaRecebida = pagamentos.Average();

    return Results.Json(new {mediaRecebida});
}).WithName("ExibirMediaRecebidaPorCredorId").WithOpenApi();

// Retorna o maior pagamento
app.MapGet("/api/pagamentos/maior/", async ([FromServices] AppDataContext contextPagamentos) =>
{
    var maiorPagamento = await contextPagamentos.Pagamentos
        .OrderByDescending(p => p.Valor.ToString())
        .FirstOrDefaultAsync();

    if (maiorPagamento == null)
    {
        return Results.NotFound();
    }

    return Results.Json(maiorPagamento);
}).WithName("ExibirMaiorPagamento").WithOpenApi();

// Retorna o menor pagamento
app.MapGet("/api/pagamentos/menor/", async ([FromServices] AppDataContext contextPagamentos) =>
{
    var menorPagamento = await contextPagamentos.Pagamentos
        .OrderBy(p => p.Valor.ToString())
        .FirstOrDefaultAsync();

    if (menorPagamento == null)
    {
        return Results.NotFound();
    }

    return Results.Json(menorPagamento);
}).WithName("ExibirMenorPagamento").WithOpenApi();

//alterar pagamentos
app.MapPut("/api/pagamentos/deletar/{id}", async ([FromRoute] int id, [FromBody] Pagamento pagamentoAtualizado, [FromServices] AppDataContext contextPagamentos) =>

{

    var pagamentoExistente = await contextPagamentos.Pagamentos.FindAsync(id);

    if (pagamentoExistente is null)

    {

        return Results.NotFound("Pagamento não localizado");

    }
    // Atualizar as propriedades do pagamentoExistente com os dados do pagamentoAtualizado
    pagamentoExistente.Valor = pagamentoAtualizado.Valor;
    pagamentoExistente.DataDePagamento = pagamentoAtualizado.DataDePagamento;
    pagamentoExistente.PedidoID = pagamentoAtualizado.PedidoID;
    pagamentoExistente.DevedorID = pagamentoAtualizado.DevedorID;
    pagamentoExistente.CredorID = pagamentoAtualizado.CredorID;

    await contextPagamentos.SaveChangesAsync();

    return Results.Ok(pagamentoAtualizado);

}).WithName("PagamentoAtualizado").WithOpenApi();

//deletar pagamentos
app.MapDelete("/api/pagamentos/deletar/{id}", async ([FromRoute] int id, [FromServices] AppDataContext contextPagamentos) =>
{
    var pagamento = await contextPagamentos.Pagamentos.FindAsync(id);
    if (pagamento is null)
    {
        return Results.NotFound("Pagamento não localizada");
    }
    contextPagamentos.Pagamentos.Remove(pagamento);
    await contextPagamentos.SaveChangesAsync();

    return Results.Ok(pagamento);
}).WithName("DeletarPagamento").WithOpenApi();


app.MapGet("/api/pessoas/credor/resumo/{id}", async ([FromServices] AppDataContext context, int id) =>
{
// Busca os pedidos onde a pessoa é credora (usando CredorID)
var pedidos = await context.Pedidos.Where(p => p.CredorID == id).ToListAsync(); 


if (pedidos == null || !pedidos.Any())
{
    return Results.NotFound($"Nenhum pedido encontrado para o Credor com ID {id}");
}

// Busca as informações da pessoa
var pessoa = await context.Pessoas.FindAsync(id);

if (pessoa == null)
{
    return Results.NotFound($"Pessoa com ID {id} não foi encontrada");
}

var resumoCredor = new
{
    Id = pessoa.ID,
    Nome = pessoa.Nome,
    QuantidadePedidos = pedidos.Count,
    ValorTotalPedidos = pedidos.Sum(p => p.ValorTotal)
};

return Results.Ok(resumoCredor);

}).WithName("ExibirResumoCredor").WithOpenApi();

app.UseCors("AcessoTotal");
app.Run();
