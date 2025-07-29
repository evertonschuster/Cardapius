#!/usr/bin/env bash
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SOLUTION="$REPO_ROOT/src/Core/Cardapius.sln"

# Restore and build the solution

dotnet restore "$SOLUTION"
dotnet build "$SOLUTION" --configuration Release --no-restore

# Find and execute all test projects
find "$REPO_ROOT/src" -name '*UnitTest*.csproj' | while read -r csproj; do
  echo "Running tests for $csproj"
  dotnet test "$csproj" --configuration Release --no-build --verbosity normal
done
