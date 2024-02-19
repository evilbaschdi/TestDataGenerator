dotnet clean
dotnet restore
dotnet build

Set-Location TestDataGenerator.Wpf
.\publish.ps1

Set-Location ..

Set-Location TestDataGenerator.Avalonia
.\publish.ps1

Set-Location ..