# TodoList API - Documentação

## Visão Geral
API para gerenciamento de tarefas desenvolvida em .NET 8 com arquitetura limpa e padrões modernos.

## Tecnologias Utilizadas
.NET 8 (ASP.NET Core Web API)

Entity Framework Core

MediatR (padrão Mediator/CQRS)

SQL Server (Docker)

Swagger/OpenAPI

## Diferenciais implementados
A API implementa paginação robusta no endpoint de listagem de tarefas:

Sistema de filtragem flexível para tarefas concluídas:

Sistema de validação usando DataAnnotation:

 Configuração avançada de logging com Serilog: (Logs detalhados facilitam debugging e monitoramento)

## Estrutura de Pastas
```
TodoList.Api/
├── Controllers/           # Endpoints da API
├── DTOs/                  # Data Transfer Objects
├── Models/                # Entidades (Task, User)
├── Data/                  # DbContext
├── Migration/             # Migrations
├── Application/           # Handlers MediatR (Commands e Queries)
├── Properties/            # Configurações do projeto
└── Program.cs             # Configurações principais e DI
```

# Pré-requisitos
Antes de executar o projeto, instale:

- .NET 8 SDK
- Docker e Docker Compose

# Configuração do Banco de Dados
### Opção 1: Usar Docker (Recomendado)
Suba o SQL Server em um container.
Na raiz do projeto onde está o arquivo docker-compose rode:

```
docker-compose up -d
```

###  Opção 2: SQL Server Local
Se você já tem o SQL Server instalado localmente, altere a string de conexão no `appsettings.json` para refletir seu ambiente.

### Configuração do AppSettings
Crie/edite o arquivo `appsettings.Development.json` e `appsettings.json` com o seguinte conteúdo:

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=TodoListDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "UmaChaveSuperSecretaDeTeste123!",
    "Issuer": "TodoListApi",
    "Audience": "TodoListApiUsers",
    "ExpireMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

> **Nota de segurança:**  
> Em ambiente de produção, utilize variáveis de ambiente ou um gerenciador de segredos para armazenar credenciais sensíveis.

## Executando o Projeto
### 1. Restaurar pacotes NuGet
Na pasta do projeto rode no terminal:
```
dotnet restore
```

### 2. Aplicar migrations do EF Core
```
dotnet ef database update
```

### 3. Executar a API
```
dotnet run
```

### 4. Acessar a documentação
A API estará disponível com documentação Swagger em:

👉 https://localhost:5001/swagger (A porta vai depender da credencial que está utilizando para rodar: venja em `Properties/launchSettings.json`)
