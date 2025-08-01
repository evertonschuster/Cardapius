#!/usr/bin/env bash
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SOLUTION="$REPO_ROOT/src/Core/Cardapius.sln"
COVERAGE_OUTPUT="$REPO_ROOT/coverage-report"

# Limpa relatórios antigos e cria pasta de saída
rm -rf "$COVERAGE_OUTPUT"
mkdir -p "$COVERAGE_OUTPUT"

# Instala o Coverlet console global tool
echo "🔧 Instalando Coverlet console..."
dotnet tool install --global coverlet.console || true
export PATH="$HOME/.dotnet/tools:$PATH"

# Descobre todos os projetos de teste
mapfile -d '' test_projects < <(find "$REPO_ROOT/src" -name '*UnitTest*.csproj' -print0)

# Executa cobertura via Coverlet console
for csproj in "${test_projects[@]}"; do
  project_name=$(basename "${csproj%.*}")
  echo "🧪 Executando cobertura para: $project_name"
  
  # Caminho do assembly de teste
  dll_path="$(dirname "$csproj")/bin/Release/net9.0/${project_name}.dll"
  
  # Executa Coverlet gerando um arquivo por projeto
  coverlet "$dll_path" \
    --target "dotnet" \
    --targetargs "test \"$csproj\" --no-build --configuration Release" \
    --format opencover \
    --output "$COVERAGE_OUTPUT/coverage-${project_name}.xml"
done
