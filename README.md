# Controle de pagamentos

## Projeto Final - Parte I - Tópicos Especiais em Sistemas

## Como Rodar o Projeto

### Pré-requisitos
Certifique-se de ter o SDK do .NET Core 8.0 instalado em sua máquina. Você pode baixá-lo em dotnet.microsoft.com.

#### Passos
Verificar Instalação do Entity Framework Core: Antes de prosseguir, verifique se o Entity Framework Core está instalado globalmente em sua máquina. Se não estiver instalado, você pode instalá-lo executando o seguinte comando:

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
