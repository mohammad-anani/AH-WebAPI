# ---------------------------
# Stage 1: Build & Publish
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app/AH.API

COPY AH.API/AH.API.sln ./
COPY AH.API/AH.API.csproj ./
COPY AH.Application/AH.Application.csproj ../AH.Application/
COPY AH.Domain/AH.Domain.csproj ../AH.Domain/
COPY AH.Infrastructure/AH.Infrastructure.csproj ../AH.Infrastructure/
COPY AH.Tests/AH.Tests.csproj ../AH.Tests/

# Restore dependencies
RUN dotnet restore AH.API.sln

# Copy all source code
COPY AH.API/ ./
COPY AH.Application/ ../AH.Application/
COPY AH.Domain/ ../AH.Domain/
COPY AH.Infrastructure/ ../AH.Infrastructure/
COPY AH.Tests/ ../AH.Tests/

# Publish the API project
RUN dotnet publish AH.API.csproj -c Release -o /out

# ---------------------------
# Stage 2: Runtime
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /app/AH.API/out ./

# Environment variables (non-sensitive only)
ENV DOTNET_ENVIRONMENT=Docker
ENV ASPNETCORE_URLS=https://+:7076;http://+:5084

# Expose ports
EXPOSE 5084 7076

# Run the API
ENTRYPOINT ["dotnet", "AH.API.dll"]
