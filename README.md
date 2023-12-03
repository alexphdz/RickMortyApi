# Proyecto RickAndMortyApi

## Requerimientos

- [.NET 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)
- [SQL Server](https://www.microsoft.com/sql-server) (puedes usar una instancia local o en Docker)
- [MYSQL] para el caso de docker ya se encuentra configurado.

## Configuración

1. Clona este repositorio:

    ```bash
    git clone https://github.com/tu-usuario/RickAndMortyApi.git
    ```

2. Abre el proyecto en tu entorno de desarrollo favorito.

3. Configura la cadena de conexión en `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "RickMortyDbContext": "Server=localhost;Database=TuBaseDeDatos;User=TuUsuario;Password=TuPassword;"
      }
    }
    ```

## Base de Datos

1. Restaura los paquetes de NuGet:

    ```bash
    dotnet restore
    ```

2. Ejecuta las migraciones para crear la base de datos:

    ```bash
    dotnet ef database update
    ```

## Ejecución

1. Abre la terminal y navega hasta la carpeta del proyecto.

2. Ejecuta la aplicación:

    ```bash
    dotnet run
    ```
## Ejecución con Docker 

1. Configura la cadena de conexión en `appsettings.json`:

    ```json
    {
      "ConnectionStrings": {
        "RickMortyDbContext": "Server=sql-server,1433;Database=RickMortyDB;User=SA;Password=MiPassword123!;"
      }
    }
    ```
2. Ejecuta la aplicación:

    ```bash
    docker-compose up --build 
    ```

La aplicación estará disponible en [https://localhost:5001](https://localhost:5001) o 8080 con Docker.



