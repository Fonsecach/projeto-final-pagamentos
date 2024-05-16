# Projeto de pagamentos

Este projeto é uma aplicação de gerenciamento de pessoas, pedidos e pagamentos, construída utilizando o framework ASP.NET Core. Ele fornece uma API RESTful para cada uma dessas entidades, permitindo operações de cadastro, consulta, atualização e exclusão(CRUD).

## Classes

<img width="3184" alt="Untitled(2)" src="https://github.com/Fonsecach/projeto-final-pagamentos/assets/113487188/a4c8e767-a844-4906-acfa-1ac510246f72">

## Funcionalidades Principais

- *Gerenciamento de Pessoas*
  - Cadastro de pessoas, permitindo adicionar múltiplas pessoas de uma vez.
  - Consulta de todas as pessoas cadastradas.
  - Consulta de pessoa por ID.
  - Consulta de pessoa por nome.
  - Atualização de dados de uma pessoa existente.
  - Exclusão de uma pessoa por ID.

- *Gerenciamento de de Pedidos*
  - Cadastro de pedidos, permitindo adicionar múltiplos pedidos de uma vez.
  - Consulta de todos os pedidos cadastrados.
  - Consulta de pedido por ID.
  - Consulta de pedido por nome.
  - Consulta de pedidos aguardando pagamento.
  - Consulta de pedidos vencidos.
  - Consulta de pedidos pagos.
  - Atualização de dados de um pedido existente.
  - Exclusão de um pedido por ID.

- *Gerenciamento de de Pagamentos*
  - Cadastro de pagamentos, associando-os a um pedido específico.
  - Consulta de todos os pagamentos cadastrados.
  - Consulta de pagamentos recebidos por ID da pessoa.
  - Consulta da média de pagamentos recebidos por ID da pessoa.
  - Consulta do maior pagamento registrado.
  - Consulta do menor pagamento registrado.
  - Atualização de dados de um pagamento existente.
  - Exclusão de um pagamento por ID.

- *Geração de relatorio de por credor*
  - Exibição de um resumo para credor, incluindo o número total de pedidos e o valor total dos pedidos.

#### Endpoint para cadastrar produtos

![POSTpagamento](https://github.com/Fonsecach/projeto-final-pagamentos/assets/113487188/77f8848f-1f32-442f-86fe-8ae4c657c3b6)

**Teste do Endpoint no ClientRest do Vs Code**

![PostpagamentoReturn201](https://github.com/Fonsecach/projeto-final-pagamentos/assets/113487188/b569cd62-2dcd-4a33-a1af-40e955b830b6)

**Retorno da busca do pedido atualizado, utilizando o parametro Id n. 7 com o swaggerUI**

![GetConsultaPedidoId7comStatusAtualizado](https://github.com/Fonsecach/projeto-final-pagamentos/assets/113487188/bbc72d29-5753-437b-a18d-3770b8042557)

#### Endpoint que exibe um objeto com resumo dos pedidos por CredorID

![GETresumoCredor](https://github.com/Fonsecach/projeto-final-pagamentos/assets/113487188/5eb4692b-7604-410a-bbd2-2f2e19ccb497)

**Teste do Endpoint com o SwaggerUI, utilizando o parametro CredorID n. 8**

![TesteResumoCliente](https://github.com/Fonsecach/projeto-final-pagamentos/assets/113487188/42b942c2-080b-4eb4-b9c1-0153896c709f)

## Como Rodar o Projeto

#### Pré-requisitos
Certifique-se de ter o SDK do .NET Core 8.0 instalado em sua máquina. Você pode baixá-lo em dotnet.microsoft.com.

##### Passos
Confirmação de Instalação do Entity Framework Core: Certifique-se de que o EntityFrameworkCore.Sqlite 8.0.4 e o EntityFrameworkCore.Design 8.0.4 estejam instalados globalmente em seu sistema. Caso esses componentes ainda não estejam instalados, você pode proceder com a instalação executando os comandos a seguir:

```
dotnet tool install --global dotnet-ef
```
Restaurar Dependências do Projeto: Após verificar a instalação do Entity Framework Core, restaure as dependências do projeto executando o comando dotnet restore no terminal, dentro do diretório raiz do projeto:

```
dotnet restore
```
Rodar o Projeto: Com as dependências restauradas, você pode compilar e rodar o projeto usando o comando:

```
dotnet watch -o
```

Acessar a API: Após iniciar o projeto, a API estará disponível em https://localhost:5001 por padrão. Você pode acessar a documentação da API em https://localhost:5001/swagger para explorar e testar os endpoints disponíveis.

Recomendamos a utilização do arquivo **ControleDePAgamentos.http** com a extensão REST CLIENT do Vs Code, para testar as requisições **POST** e **PUT**, enquanto as demais recomendamos a utilização da UI do Swagger.

Em um cenário de desenvolvimento poderá excluir o Sqlite e Migrations para recriar um banco de dados, basta rodar os comandos:

Criar a migração
```
dotnet ef migrations add InitialCreate
```
Criar o banco de dados
```
dotnet ef database update
```

Com esses passos, você estará pronto para executar o projeto e começar a utilizar suas funcionalidades de gerenciamento de pessoas, pedidos e pagamentos.

