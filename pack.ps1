if (Test-Path ".\Fynydd.Sfumato\nupkg") { Remove-Item ".\Fynydd.Sfumato\nupkg" -Recurse -Force }
. ./clean.ps1
Set-Location Fynydd.Sfumato
dotnet pack --configuration Release
Set-Location ..
