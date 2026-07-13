# ===========================
# Build Stage
# ===========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy solution file
COPY StudentManagementSystem.sln .

# Copy project files
COPY StudentManagement.API/StudentManagement.API.csproj StudentManagement.API/
COPY StudentManagement.Application/StudentManagement.Application.csproj StudentManagement.Application/
COPY StudentManagement.Domain/StudentManagement.Domain.csproj StudentManagement.Domain/
COPY StudentManagement.Infrastructure/StudentManagement.Infrastructure.csproj StudentManagement.Infrastructure/

# Restore dependencies
RUN dotnet restore StudentManagementSystem.sln

# Copy remaining source code
COPY . .

# Publish API
RUN dotnet publish StudentManagement.API/StudentManagement.API.csproj \
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

ENTRYPOINT ["dotnet", "StudentManagement.API.dll"]
