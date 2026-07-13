# ===========================
# Build Stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution and project files
COPY StudentManagementSystem.sln .

COPY StudentManagementSystem/StudentManagementSystem.csproj StudentManagementSystem/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restore dependencies
RUN dotnet restore StudentManagementSystem.sln

# Copy the remaining source
COPY . .

# Publish the API
RUN dotnet publish StudentManagementSystem/StudentManagementSystem.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ===========================
# Runtime Stage
# ===========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "StudentManagementSystem.dll"]
