# Sentinel Service

Servidor de identidade baseado em ASP.NET Core e OpenIddict. Emite e valida tokens JWT para múltiplos tipos de clientes e protege APIs internas.

## Requisitos
- .NET SDK 9.0
- PostgreSQL

## Executando

### dotnet run
```bash
cd src/Core/Services/Sentinel/Adapters/Driving/Sentinel.Api
dotnet run
```

### Docker + Aspire
Exemplo (ajuste conforme necessário):
```bash
docker build -t sentinel .
docker run -p 5000:8080 sentinel
```

### Atualizar banco de dados
```bash
dotnet ef database update --project Adapters/Driving/Sentinel.Api/Sentinel.Api.csproj
```

## Fluxos de teste com curl

### Obter token (client credentials)
```bash
curl -X POST http://localhost:5000/connect/token \
 -H "Content-Type: application/x-www-form-urlencoded" \
 -d "grant_type=client_credentials&client_id=console&client_secret=secret&scope=api"
```

### Login com usuário e senha (password flow)
```bash
curl -X POST http://localhost:5000/connect/token \
 -H "Content-Type: application/x-www-form-urlencoded" \
 -d "grant_type=password&client_id=console&client_secret=secret&scope=api&username=usuario@example.com&password=Senha123$"
```

### Usar refresh token
```bash
curl -X POST http://localhost:5000/connect/token \
 -H "Content-Type: application/x-www-form-urlencoded" \
 -d "grant_type=refresh_token&client_id=console&client_secret=secret&refresh_token={token}"
```

### Chamar API protegida
```bash
curl http://localhost:5000/api/todos -H "Authorization: Bearer {access_token}"
```

## Health Check
`/health`

A coleção do Postman está disponível em `docs/postman_collection.json`.

### Integração com outros serviços
Outros serviços podem consumir a autenticação configurando o `JwtBearer` com o `Authority` e `Audience` do Sentinel:

```csharp
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = configuration["Sentinel:Authority"];
        options.Audience = configuration["Sentinel:Audience"];
        options.RequireHttpsMetadata = false; // apenas para desenvolvimento
    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Admin", p => p.RequireRole("Administration.Manager"));
});
```

As roles específicas de cada serviço podem ser registradas no Sentinel via `appsettings.json` (exemplo para o serviço de Administração):

```json
"Roles": {
  "Administration": [
    "Administration.Manager",
    "Administration.Viewer"
  ]
}
```
