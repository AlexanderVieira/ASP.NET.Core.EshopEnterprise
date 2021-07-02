#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/ApiGateways/ESE.Porchase.API/ESE.Porchase.API.csproj", "src/ApiGateways/ESE.Porchase.API/"]
COPY ["src/BulidingBlocks/Core/ESE.Core/ESE.Core.csproj", "src/BulidingBlocks/Core/ESE.Core/"]
COPY ["src/BulidingBlocks/MessageBus/ESE.MessageBus/ESE.MessageBus.csproj", "src/BulidingBlocks/MessageBus/ESE.MessageBus/"]
COPY ["src/BulidingBlocks/Services/ESE.WebAPI.Core/ESE.WebAPI.Core.csproj", "src/BulidingBlocks/Services/ESE.WebAPI.Core/"]

RUN dotnet restore "src/ApiGateways/ESE.Porchase.API/ESE.Porchase.API.csproj"
COPY . .
WORKDIR "src/ApiGateways/ESE.Porchase.API"
RUN dotnet build "ESE.Porchase.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESE.Porchase.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESE.Porchase.API.dll"]