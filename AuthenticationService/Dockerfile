# --- Base image ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8001
ENV ASPNETCORE_URLS=http://+:8001

# --- Build image ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Kopiér solutionfil
COPY AuthenticationService.sln ./

# Kopiér kun de projekter, API’et afhænger af
COPY AuthenticationService_Api/*.csproj AuthenticationService_Api/
COPY AuthenticationService_Application/*.csproj AuthenticationService_Application/
COPY AuthenticationService_Domain/*.csproj AuthenticationService_Domain/
COPY AuthenticationService_Infrastructure/*.csproj AuthenticationService_Infrastructure/
COPY AuthenticationService_Shared/*.csproj AuthenticationService_Shared/

# Restore dependencies
RUN dotnet restore "AuthenticationService.sln"

# Kopiér kun kildekoden til de projekter
COPY AuthenticationService_Api/ AuthenticationService_Api/
COPY AuthenticationService_Application/ AuthenticationService_Application/
COPY AuthenticationService_Domain/ AuthenticationService_Domain/
COPY AuthenticationService_Infrastructure/ AuthenticationService_Infrastructure/
COPY AuthenticationService_Shared/ AuthenticationService_Shared/

# Build API-projekt
RUN dotnet build "AuthenticationService_Api/AuthenticationService_Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# --- Publish stage ---
FROM build AS publish
RUN dotnet publish "AuthenticationService_Api/AuthenticationService_Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# --- Runtime stage ---
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthenticationService_Api.dll"]
