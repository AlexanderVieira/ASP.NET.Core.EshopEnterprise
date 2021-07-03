#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Services/Client/ESE.Client.API/ESE.Client.API.csproj", "Services/Client/ESE.Client.API/"]
COPY ["./BulidingBlocks/Services/ESE.WebAPI.Core/ESE.WebAPI.Core.csproj", "BulidingBlocks/Services/ESE.WebAPI.Core/"]
COPY ["./BulidingBlocks/Core/ESE.Core/ESE.Core.csproj", "BulidingBlocks/Core/ESE.Core/"]
COPY ["./BulidingBlocks/MessageBus/ESE.MessageBus/ESE.MessageBus.csproj", "BulidingBlocks/MessageBus/ESE.MessageBus/"]
RUN dotnet restore "Services/Client/ESE.Client.API/ESE.Client.API.csproj"
COPY . .
WORKDIR "Services/Client/ESE.Client.API"
RUN dotnet build "ESE.Client.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESE.Client.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESE.Client.API.dll"]