# WHAT IS?
**PROJECT:** Microservice Template.

This project is developed with following tecnologies:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [MSSQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

# HOW TO MOUNT MY DEVELOPMENT ENVIRONMENT

## 1. Requiriments

* (Required) [`dotnet sdk 5.0`](https://dotnet.microsoft.com/download)
* (Required) [`wsl 2`](https://docs.microsoft.com/pt-br/windows/wsl/install-win10) - only Windows for docker use
* (Required) [`docker`](https://hub.docker.com/editions/community/docker-ce-desktop-windows)
* (Required) [`mssql server`](https://hub.docker.com/r/microsoft/mssql-server-linux) - how to use mssql in a container docker
* (Optional) [`vscode`](https://code.visualstudio.com/download) - preferencial IDE for development
* (Optional) [`windows terminal`](https://docs.microsoft.com/pt-br/windows/terminal/get-started)

## 2. Mounting Development Environment

1. Install dotnet 5.0:

```powershell
choco install dotnet-sdk --version=5.0.202
```

2. Enable WSL 2

```powershell
Enable-WindowsOptionalFeature -FeatureName Microsoft-Windows-Subsystem-Linux -Online
```

```powershell
Enable-WindowsOptionalFeature -FeatureName VirtualMachinePlatform -Online
```

```powershell
msiexec /i https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi
```

```powershell
wsl --set-default-version 2
```

(Download Distro Linux)[https://docs.microsoft.com/pt-br/windows/wsl/install-manual]

```powershell
Invoke-WebRequest -UseBasicParsing -Uri https://aka.ms/wslubuntu2004 -OutFile ubuntu2004.appx
Add-AppxPackage ./ubuntu2004.appx
```

3. Install Docker

```powershell
Enable-WindowsOptionalFeature -FeatureName Containers -Online
```

Follow the instructions in: https://docs.docker.com/docker-for-windows/install/

4. MSSQL Server for Development

```powershell
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=[PASSWORD IN DEV]' -p 1433:1433 --name mssql -d mcr.microsoft.com/mssql/server:2017-CU8-ubuntu
```

## Environment Variables

For the development environment, declare the following variable so that the dotnet interprets the settings in the appsettings.Development.json file. 

In actual context or process (restart is no necessary)
```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
```

In user context (logoff is necessary)
```powershell
[System.Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT","Development","User")
```

Or In machine context (restart is necessary)
```powershell
[System.Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT","Development","Machine")
```

## Migrations

For create migrations for IdentityContext execute following commands in your development environment in root folder of the project.

```powershell
dotnet ef migrations add first -p .\src\infrastructure\Infrastructure.CrossCutting.Data\Infrastructure.CrossCutting.Data.csproj --startup-project .\src\services\Services\Services.csproj --context IdentityContext
```

```powershell
dotnet ef database update -p .\src\infrastructure\Infrastructure.CrossCutting.Data\Infrastructure.CrossCutting.Data.csproj --startup-project .\src\services\Services\Services.csproj --context IdentityContext -v
```

## How to build this project

To build this project on a container image, we need to run the following Docker commands at the prompt (bash or powershell):

```powershell
docker build -t laca:0.0.1 -f .\Dockerfile .
```

To run this project after build run the following Docker command:

```powershell
docker run -e 'ASPNETCORE_ENVIRONMENT=Development' -p 5001:443 -p 5000:80 -d --link mssql --name laca laca:0.0.1
```

In your browser access this address:

http://localhost:5000/swagger