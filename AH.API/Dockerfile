# ---------------------------
# Stage 1: Build & Publish
# ---------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project folders
COPY AH.API/ AH.API/
COPY AH.Application/ AH.Application/
COPY AH.Domain/ AH.Domain/
COPY AH.Infrastructure/ AH.Infrastructure/

# Restore dependencies
WORKDIR /app/AH.API
RUN dotnet restore AH.API.sln

# Publish
RUN dotnet publish AH.API.csproj -c Release -o /out

# ---------------------------
# Stage 2: Runtime
# ---------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published output
COPY --from=build /out .

ENV DOTNET_ENVIRONMENT=Docker
ENV JWT_KEY="2000200120022003"
ENV JWT_EXPIRE_IN_MINUTES="1"
ENV REFRESH_TOKEN_EXPIRE_IN_MINUTES="60"

# Expose default HTTP port
EXPOSE 7076

# Run the API
ENTRYPOINT ["dotnet", "AH.API.dll"]
