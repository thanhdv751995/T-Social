#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
ENV TZ="Asia/Ho_Chi_Minh"
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Extention.Management.HttpApi.Host/Extention.Management.HttpApi.Host.csproj", "src/Extention.Management.HttpApi.Host/"]
COPY ["src/Extention.Management.EntityFrameworkCore.DbMigrations/Extention.Management.EntityFrameworkCore.DbMigrations.csproj", "src/Extention.Management.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Extention.Management.EntityFrameworkCore/Extention.Management.EntityFrameworkCore.csproj", "src/Extention.Management.EntityFrameworkCore/"]
COPY ["src/Extention.Management.Domain/Extention.Management.Domain.csproj", "src/Extention.Management.Domain/"]
COPY ["src/Extention.Management.Domain.Shared/Extention.Management.Domain.Shared.csproj", "src/Extention.Management.Domain.Shared/"]
COPY ["src/Extention.Management.Application/Extention.Management.Application.csproj", "src/Extention.Management.Application/"]
COPY ["src/Extention.Management.Application.Contracts/Extention.Management.Application.Contracts.csproj", "src/Extention.Management.Application.Contracts/"]
COPY ["src/Extention.Management.HttpApi/Extention.Management.HttpApi.csproj", "src/Extention.Management.HttpApi/"]
RUN dotnet restore "src/Extention.Management.HttpApi.Host/Extention.Management.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/Extention.Management.HttpApi.Host"
RUN dotnet build "Extention.Management.HttpApi.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Extention.Management.HttpApi.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Extention.Management.HttpApi.Host.dll"]