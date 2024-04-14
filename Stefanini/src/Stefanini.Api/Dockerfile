#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Stefanini.API/Stefanini.API.csproj", "src/Stefanini.API/"]
COPY ["src/Stefanini.Application/Stefanini.Application.csproj", "src/Stefanini.Application/"]
COPY ["src/Stefanini.Domain/Stefanini.Domain.csproj", "src/Stefanini.Domain/"]
COPY ["src/Stefanini.Infra.Data/Stefanini.Infra.Data.csproj", "src/Stefanini.Infra.Data/"]
RUN dotnet restore "./src/Stefanini.API/Stefanini.API.csproj"
COPY . .
WORKDIR "/src/src/Stefanini.API"
RUN dotnet build "./Stefanini.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Stefanini.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stefanini.API.dll"]