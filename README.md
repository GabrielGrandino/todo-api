# TodoList API (PASSO 1)

## Como rodar (PASSO 1)
1. Certifique-se que .NET 8 SDK está instalado.
2. Na pasta raiz da solução:
   - `cd TodoList.Api`
   - `dotnet restore`
   - `dotnet run`

## Banco de Dados (SQL Server via Docker)
1. Suba o container:
   docker-compose up -d

2. Verifique se está rodando:
   docker ps

3. A string de conexão já está configurada em appsettings.json:
   Server=localhost,1433;Database=TodoListDb;User Id=sa;Password=Your_password123;TrustServerCertificate=True;

cd TodoList.Api
dotnet ef migrations add InitialCreate -o Migrations
dotnet ef database update
