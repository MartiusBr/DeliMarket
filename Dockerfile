#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["DeliMarket/DeliMarket/Server/DeliMarket.Server.csproj", "DeliMarket/DeliMarket/Server/"]
COPY ["DeliMarket/DeliMarket/Shared/DeliMarket.Shared.csproj", "DeliMarket/DeliMarket/Shared/"]
COPY ["DeliMarket.Tienda/DeliMarket.Tienda.csproj", "DeliMarket.Tienda/"]
COPY ["DeliMarket/DeliMarket/Client/DeliMarket.Client.csproj", "DeliMarket/DeliMarket/Client/"]
RUN dotnet restore "DeliMarket/DeliMarket/Server/DeliMarket.Server.csproj"
COPY . .
WORKDIR "/src/DeliMarket/DeliMarket/Server"
RUN dotnet build "DeliMarket.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeliMarket.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeliMarket.Server.dll"]