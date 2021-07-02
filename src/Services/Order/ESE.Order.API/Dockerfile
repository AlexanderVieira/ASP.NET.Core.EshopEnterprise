#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Services/Order/ESE.Order.API/ESE.Order.API.csproj", "Services/Order/ESE.Order.API/"]
COPY ["./BulidingBlocks/Core/ESE.Core/ESE.Core.csproj", "BulidingBlocks/Core/ESE.Core/"]
COPY ["./BulidingBlocks/MessageBus/ESE.MessageBus/ESE.MessageBus.csproj", "BulidingBlocks/MessageBus/ESE.MessageBus/"]
COPY ["./BulidingBlocks/Services/ESE.WebAPI.Core/ESE.WebAPI.Core.csproj", "BulidingBlocks/Services/ESE.WebAPI.Core/"]

RUN dotnet restore "Services/Order/ESE.Order.API/ESE.Order.API.csproj"
COPY . .
WORKDIR "Services/Order/ESE.Order.API"
RUN dotnet build "ESE.Order.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESE.Order.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESE.Order.API.dll"]