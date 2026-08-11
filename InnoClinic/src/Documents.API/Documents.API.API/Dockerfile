FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "./"]
COPY ["src/Documents.API/Documents.API.API/InnoClinic.Documents.API.csproj", "src/Documents.API/Documents.API.API/"]
COPY ["src/Documents.API/Documents.API.Application/InnoClinic.Documents.API.Application.csproj", "src/Documents.API/Documents.API.Application/"]
COPY ["src/Documents.API/Documents.API.Domain/InnoClinic.Documents.API.Domain.csproj", "src/Documents.API/Documents.API.Domain/"]
COPY ["src/Documents.API/Documents.API.Infrastructure/InnoClinic.Documents.API.Infrastructure.csproj", "src/Documents.API/Documents.API.Infrastructure/"]
COPY ["src/Shared/InnoClinic.Shared.csproj", "src/Shared/"]
RUN dotnet restore "./src/Documents.API/Documents.API.API/InnoClinic.Documents.API.csproj"
COPY . .
WORKDIR "/src/src/Documents.API/Documents.API.API"
RUN dotnet build "./InnoClinic.Documents.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InnoClinic.Documents.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoClinic.Documents.API.dll"]