# ---------------------------
# Stage 1: Build & Publish
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files first for better Docker layer caching
COPY AH.API/AH.API.sln ./
COPY AH.API/AH.API.csproj ./AH.API/
COPY AH.Application/AH.Application.csproj ./AH.Application/
COPY AH.Domain/AH.Domain.csproj ./AH.Domain/
COPY AH.Infrastructure/AH.Infrastructure.csproj ./AH.Infrastructure/
COPY AH.Tests/AH.Tests.csproj ./AH.Tests/

# Copy solution file to AH.API directory (where it expects to be)
COPY AH.API/AH.API.sln ./AH.API/

# Restore dependencies using solution file from AH.API directory
WORKDIR /app/AH.API
RUN dotnet restore AH.API.sln

# Go back to root and copy all source code
WORKDIR /app
COPY AH.API/ ./AH.API/
COPY AH.Application/ ./AH.Application/
COPY AH.Domain/ ./AH.Domain/
COPY AH.Infrastructure/ ./AH.Infrastructure/
COPY AH.Tests/ ./AH.Tests/

# Publish the API project
RUN dotnet publish AH.API/AH.API.csproj -c Release -o /out

# ---------------------------
# Stage 2: Runtime
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /out .

# Set environment variables with defaults (non-sensitive only)
ENV DOTNET_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS=https://+:7076;http://+:5084

# Expose ports (matching your configuration)
EXPOSE 5084
EXPOSE 7076

# Run the API
ENTRYPOINT ["dotnet", "AH.API.dll"]