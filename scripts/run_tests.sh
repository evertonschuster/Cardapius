#!/usr/bin/env bash
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SOLUTION="$REPO_ROOT/src/Core/Cardapius.sln"

dotnet restore "$SOLUTION"
dotnet build "$SOLUTION" --configuration Release --no-restore

# Alternativa segura utilizando mapfile
mapfile -d '' test_projects < <(find "$REPO_ROOT/src" -name '*UnitTest*.csproj' -print0)

for csproj in "${test_projects[@]}"; do
  echo "Running tests for $csproj"
  dotnet test "$csproj" --configuration Release --no-build --verbosity normal
done
