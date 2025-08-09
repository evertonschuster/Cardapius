param(
    [Parameter(Mandatory=$true)][string]$Project,
    [Parameter(Mandatory=$true)][string]$Migration
)

$repoRoot = (Resolve-Path "$PSScriptRoot/.." ).Path

switch ($Project) {
    "Administration" {
        $projectPath = Join-Path $repoRoot "src/Core/Services/Administration/Adapters/Driven/Administration.Infra.DataBase.EntityFramework"
    }
    Default {
        Write-Error "Projeto desconhecido: $Project"
        exit 1
    }
}

Write-Host "🔧 Instalando dotnet-ef (se necessario)..."
dotnet tool install --global dotnet-ef | Out-Null

Write-Host "📦 Restaurando dependencias..."
dotnet restore $projectPath

Write-Host "📚 Criando migracao '$Migration' para $Project..."
dotnet ef migrations add $Migration --project $projectPath

Write-Host "✅ Migracao criada com sucesso."
