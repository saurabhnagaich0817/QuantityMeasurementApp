# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy all project files
COPY ["QuantityMeasurementApp.API/QuantityMeasurementApp.API.csproj", "QuantityMeasurementApp.API/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["RepoLayer/RepoLayer.csproj", "RepoLayer/"]
COPY ["ModelLayer/ModelLayer.csproj", "ModelLayer/"]

# Restore packages
RUN dotnet restore "QuantityMeasurementApp.API/QuantityMeasurementApp.API.csproj"

# Copy everything else
COPY . .

# Build & publish
RUN dotnet publish "QuantityMeasurementApp.API/QuantityMeasurementApp.API.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5109

ENTRYPOINT ["dotnet", "QuantityMeasurementApp.API.dll"]
