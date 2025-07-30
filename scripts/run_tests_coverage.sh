#!/usr/bin/env bash
set -euo pipefail

REPO_ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
SOLUTION="$REPO_ROOT/src/Core/Cardapius.sln"

# Install reportgenerator for coverage conversion
if ! command -v reportgenerator &> /dev/null; then
  dotnet tool install --global dotnet-reportgenerator-globaltool > /dev/null
  export PATH="$PATH:$HOME/.dotnet/tools"
fi

dotnet restore "$SOLUTION"
dotnet build "$SOLUTION" --configuration Release --no-restore

# Find all test projects under src/**/test automatically
mapfile -d '' test_projects < <(find "$REPO_ROOT/src" -path '*/test/*' -name '*Test*.csproj' -print0)

RESULTS_DIR="$REPO_ROOT/TestResults"
mkdir -p "$RESULTS_DIR"

for csproj in "${test_projects[@]}"; do
  echo "Running tests for $csproj"
  dotnet test "$csproj" \
    --configuration Release \
    --no-build \
    --collect:"XPlat Code Coverage" \
    --results-directory "$RESULTS_DIR"
done

# Merge Cobertura files into OpenCover format for SonarCloud
reportgenerator \
  -reports:"$RESULTS_DIR/**/coverage.cobertura.xml" \
  -targetdir:"$RESULTS_DIR" \
  -reporttypes:Opencover > /dev/null

echo "Coverage report generated at $RESULTS_DIR/coverage.opencover.xml"