# Projeto de pagamentos

Este projeto é uma aplicação de gerenciamento de pessoas, pedidos e pagamentos, construída utilizando o framework ASP.NET Core. Ele fornece uma API RESTful para cada uma dessas entidades, permitindo operações de cadastro, consulta, atualização e exclusão(CRUD).

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

### Como Rodar o Projeto

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
Criar o manco de dados
```
dotnet ef migrations add InitialCreate
```

Com esses passos, você estará pronto para executar o projeto e começar a utilizar suas funcionalidades de gerenciamento de pessoas, pedidos e pagamentos.


Descrever as funcionalidades

<img width="3200" alt="Untitled" src="https://github.com/Fonsecach/projetocrud/assets/113487188/045e93d5-3260-407c-addf-ba1ffd7e0256">
