# Sentinel Service

Servidor de identidade baseado em ASP.NET Core e OpenIddict. Emite e valida tokens JWT para múltiplos tipos de clientes e protege APIs internas.

## Requisitos
- .NET SDK 9.0
- SQL Server (padrão, utiliza InMemory no desenvolvimento)

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
