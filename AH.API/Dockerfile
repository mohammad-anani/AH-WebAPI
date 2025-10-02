# ---------------------------
# Stage 1: Build & Publish
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory to AH.API inside container
WORKDIR /src/AH.API

# Copy solution and project files first for caching
COPY AH.API/AH.API.sln ./AH.API.sln
COPY AH.API/AH.API.csproj ./AH.API.csproj

# Copy sibling project .csproj files
COPY AH.Application/AH.Application.csproj ../AH.Application/AH.Application.csproj
COPY AH.Domain/AH.Domain.csproj ../AH.Domain/AH.Domain.csproj
COPY AH.Infrastructure/AH.Infrastructure.csproj ../AH.Infrastructure/AH.Infrastructure.csproj
COPY AH.Tests/AH.Tests.csproj ../AH.Tests/AH.Tests.csproj

# Restore dependencies
RUN dotnet restore AH.API.sln

# Copy all source code
COPY AH.API/ ./AH.API
COPY AH.Application/ ../AH.Application
COPY AH.Domain/ ../AH.Domain
COPY AH.Infrastructure/ ../AH.Infrastructure
COPY AH.Tests/ ../AH.Tests

# Publish the API project
RUN dotnet publish AH.API.csproj -c Release -o /out

# ---------------------------
# Stage 2: Runtime
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /out ./

# Environment variables
ENV DOTNET_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS=https://+:7076;http://+:5084
ENV JWT_KEY="2000200120022003"
ENV JWT_EXPIRE_IN_MINUTES="1"
ENV REFRESH_TOKEN_EXPIRE_IN_MINUTES="60"

# Expose ports
EXPOSE 5084 7076

# Run the API
ENTRYPOINT ["dotnet", "AH.API.dll"]
