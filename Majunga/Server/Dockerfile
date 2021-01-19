#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Majunga/Server/Majunga.Server.csproj", "Majunga/Server/"]
COPY ["Majunga/Client/Majunga.Client.csproj", "Majunga/Client/"]
COPY ["Majunga/Shared/Majunga.Shared.csproj", "Majunga/Shared/"]
RUN dotnet restore "Majunga/Server/Majunga.Server.csproj"
COPY . .
WORKDIR "/src/Majunga/Server"
RUN dotnet build "Majunga.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Majunga.Server.csproj" -c Release -o /app/publish
COPY ["https/Majunga.Server.pfx", "/https/Majunga.Server.pfx"]


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Majunga.Server.dll"]