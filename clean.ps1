Set-Location Fynydd.Sfumato
if (Test-Path ".\bin") { Remove-Item bin -Recurse -Force }
if (Test-Path ".\obj") { Remove-Item obj -Recurse -Force }
dotnet restore
Set-Location ..

Set-Location Fynydd.Sfumato.Tests
if (Test-Path ".\bin") { Remove-Item bin -Recurse -Force }
if (Test-Path ".\obj") { Remove-Item obj -Recurse -Force }
dotnet restore
Set-Location ..
