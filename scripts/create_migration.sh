#!/usr/bin/env bash
set -euo pipefail

# Usage: scripts/create_migration.sh <project> <migration-name>
if [ "$#" -ne 2 ]; then
  echo "Uso: $0 <projeto> <nome-da-migracao>" >&2
  exit 1
fi

PROJECT_KEY="$1"
MIGRATION_NAME="$2"

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"

case "$PROJECT_KEY" in
  Administration)
    PROJECT_PATH="$REPO_ROOT/src/Core/Services/Administration/Adapters/Driven/Administration.Infra.DataBase.EntityFramework"
    ;;
  *)
    echo "Projeto desconhecido: $PROJECT_KEY" >&2
    exit 1
    ;;
esac

# Instala dotnet-ef caso nao esteja instalado
echo "🔧 Instalando dotnet-ef (se necessario)..."
dotnet tool install --global dotnet-ef >/dev/null 2>&1 || true
export PATH="$HOME/.dotnet/tools:$PATH"

# Restaura dependencias
echo "📦 Restaurando dependencias..."
dotnet restore "$PROJECT_PATH"

# Cria a migracao
echo "📚 Criando migracao '$MIGRATION_NAME' para $PROJECT_KEY..."
dotnet ef migrations add "$MIGRATION_NAME" --project "$PROJECT_PATH"

echo "✅ Migracao criada com sucesso."
