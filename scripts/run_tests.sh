#!/usr/bin/env bash
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SOLUTION="$REPO_ROOT/src/Core/Cardapius.sln"
COVERAGE_OUTPUT="$REPO_ROOT/coverage-report"

# Limpa relatórios antigos
rm -rf "$COVERAGE_OUTPUT"
mkdir -p "$COVERAGE_OUTPUT"

echo "📦 Restaurando pacotes..."
dotnet restore "$SOLUTION"

echo "🔨 Buildando a solução..."
dotnet build "$SOLUTION" --configuration Release --no-restore

# Descobre todos os projetos de teste
mapfile -d '' test_projects < <(find "$REPO_ROOT/src" -name '*UnitTest*.csproj' -print0)

# Roda os testes com cobertura
for csproj in "${test_projects[@]}"; do
  echo "🧪 Executando testes com cobertura para: $csproj"
  dotnet test "$csproj" \
    --configuration Release \
    --no-build \
    --verbosity normal \
    --collect:"XPlat Code Coverage"
done

# Instala o reportgenerator se necessário
echo "📊 Instalando ReportGenerator..."
dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.1.26 || true
export PATH="$PATH:$HOME/.dotnet/tools"

# Descobre todos os arquivos cobertura.cobertura.xml
echo "🔍 Procurando arquivos de cobertura..."
coverage_files=$(find "$REPO_ROOT" -type f -name 'coverage.cobertura.xml')
echo "Arquivos de cobertura encontrados: $coverage_files"

if [[ -z "$coverage_files" ]]; then
  echo "❌ Nenhum arquivo de cobertura encontrado. Verifique se os testes foram executados corretamente."
  exit 1
fi

echo "📊 Gerando relatório de cobertura em formato OpenCover..."
reportgenerator \
  -reports:$coverage_files \
  -targetdir:"$COVERAGE_OUTPUT" \
  -reporttypes:OpenCover \
  -filefilters:"+*.cs" \
  -verbosity:Error

echo "✅ Relatório de cobertura gerado em: $COVERAGE_OUTPUT/opencover.xml"
