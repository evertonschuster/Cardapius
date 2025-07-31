# Cardapius

Aplicação exemplo baseada no modelo Modular Monolith with DDD. O projeto utiliza ASP.NET Core e está organizado em módulos.

## Pré-requisitos

- [.NET SDK 9.0](https://dotnet.microsoft.com/download) (versão indicada em `global.json`)
- [Node.js](https://nodejs.org) para os front-ends

## Como compilar

1. Restaure as dependências:
   ```bash
   dotnet restore src/Core/Cardapius.sln
   ```
2. Compile a solução:
   ```bash
   dotnet build src/Core/Cardapius.sln --no-restore
   ```

## Executando os testes

Para rodar todos os testes automatizados utilize:

```bash
dotnet test src/Core/Cardapius.sln
```

Caso não possua o .NET SDK instalado, consulte a documentação oficial para instalação ou utilize o script `dotnet-install.sh` disponibilizado pela Microsoft.


![CodeRabbit Pull Request Reviews](https://img.shields.io/coderabbit/prs/github/evertonschuster/Cardapius?utm_source=oss&utm_medium=github&utm_campaign=evertonschuster%2FCardapius&labelColor=171717&color=FF570A&link=https%3A%2F%2Fcoderabbit.ai&label=CodeRabbit+Reviews)