# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy solution file
COPY AH.API/AH.API.sln ./

# Copy all project files for restore
COPY AH.API/AH.API.csproj ./AH.API/
COPY AH.Application/AH.Application.csproj ./AH.Application/
COPY AH.Domain/AH.Domain.csproj ./AH.Domain/
COPY AH.Infrastructure/AH.Infrastructure.csproj ./AH.Infrastructure/
COPY AH.Tests/AH.Tests.csproj ./AH.Tests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . ./

# Build and publish the API project
RUN dotnet publish AH.API/AH.API.csproj -c Release -o /out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# Expose the API ports (matching launchSettings.json)
EXPOSE 5084
EXPOSE 7076

# Start the application
ENTRYPOINT ["dotnet", "AH.API.dll"]
