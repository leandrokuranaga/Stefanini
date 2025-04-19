# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Stefanini.API/Stefanini.API.csproj", "src/Stefanini.API/"]
COPY ["src/Stefanini.Application/Stefanini.Application.csproj", "src/Stefanini.Application/"]
COPY ["src/Stefanini.Domain/Stefanini.Domain.csproj", "src/Stefanini.Domain/"]
COPY ["src/Stefanini.Infra.Data/Stefanini.Infra.Data.csproj", "src/Stefanini.Infra.Data/"]
COPY ["src/Stefanini.Infra.CrossCutting.IoC/Stefanini.Infra.CrossCutting.IoC.csproj", "src/Stefanini.Infra.CrossCutting.IoC/"]
COPY ["src/Stefanini.Infra.Utils/Stefanini.Infra.Utils.csproj", "src/Stefanini.Infra.Utils/"]
RUN dotnet restore "./src/Stefanini.API/Stefanini.API.csproj"
COPY . .
WORKDIR "/src/src/Stefanini.API"
RUN dotnet build "./Stefanini.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Stefanini.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Stefanini.API.dll"]