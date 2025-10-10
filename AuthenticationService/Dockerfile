# --- Base image ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# --- Build image ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopiér solution-fil og projektfiler (én mappe op)
COPY ../Authentication.sln ./
COPY ../AuthenticationService_Api/*.csproj AuthenticationService_Api/
COPY ../AuthenticationService_Application/*.csproj AuthenticationService_Application/
COPY ../AuthenticationSerivce_Domain/*.csproj AuthenticationSerivce_Domain/
COPY ../AuthenticationService_Infrastructure/*.csproj AuthenticationService_Infrastructure/
COPY ../AuthenticationService_Shared/*.csproj AuthenticationService_Shared/

# Restore afhængigheder
RUN dotnet restore "Authentication.sln"

# Kopiér resten af solutionen
COPY .. .

# Byg API-projektet
RUN dotnet build "AuthenticationService_Api/AuthenticationService_Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# --- Publish stage ---
FROM build AS publish
RUN dotnet publish "AuthenticationService_Api/AuthenticationService_Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Runtime stage ---
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationService_Api.dll"]
