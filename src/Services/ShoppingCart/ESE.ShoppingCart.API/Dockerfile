#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["./Services/ShoppingCart/ESE.ShoppingCart.API/ESE.ShoppingCart.API.csproj", "Services/ShoppingCart/ESE.ShoppingCart.API/"]
COPY ["./BuildingBlocks/Services/ESE.WebAPI.Core/ESE.WebAPI.Core.csproj", "BuildingBlocks/Services/ESE.WebAPI.Core/"]
COPY ["./BuildingBlocks/Core/ESE.Core/ESE.Core.csproj", "BuildingBlocks/Core/ESE.Core/"]
COPY ["./BuildingBlocks/MessageBus/ESE.MessageBus/ESE.MessageBus.csproj", "BuildingBlocks/MessageBus/ESE.MessageBus/"]
RUN dotnet restore "Services/ShoppingCart/ESE.ShoppingCart.API/ESE.ShoppingCart.API.csproj"
COPY . .
WORKDIR "Services/ShoppingCart/ESE.ShoppingCart.API"
RUN dotnet build "ESE.ShoppingCart.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ESE.ShoppingCart.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ESE.ShoppingCart.API.dll"]