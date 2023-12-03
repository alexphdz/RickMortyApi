# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src   # Cambiado a /src

COPY . .
RUN dotnet restore

# Compila y publica la aplicación
RUN dotnet publish -c Release -o /app/out   # Cambiado a /app/out

# Etapa de publicación
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "RickAndMortyApi.dll"]
