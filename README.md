# .NET 8/9 Aspire

I am learning .NET 8/9 Aspire from different Video Courses, Books, and Websites.

## Reference(s)

> 1. <https://fiodar.substack.com/p/a-guide-to-securing-net-aspire-apps>
> 1. <https://github.com/fiodarsazanavets/dotnet-aspire-examples/tree/main>
> 1. <https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview>
> 1. <https://www.educative.io/courses/using-single-sign-on-for-securing-applications-in-aspnet-core/getting-started>

## Few Commands

```powershell
dotnet --list-sdks

dotnet workload list
dotnet workload update
dotnet workload install aspire

dotnet new list aspire
dotnet new aspire-starter --help
```

## Few Points

```text
"Server=127.0.0.1,1443;User ID=sa;Password=P@$$w0rd;TrustServerCertificate=true;Database=master"
"Server=localhost;Port=3306;User ID=root;Password=zHcjehwAs3PanKy(1KPgGX;Database=mysqldb"
```

## API URLs

> 1. <https://localhost:7324/weatherforecast>
> 1. <https://localhost:7324/weatherforecastado>
> 1. <https://localhost:7324/weatherforecastefsql>
> 1. <https://localhost:7324/weatherforecastpsql>
> 1. <https://localhost:7324/weatherforecastmysql>

## MySQL

```sql
-- Create the WeatherDb database (if it doesn't exist)
CREATE DATABASE IF NOT EXISTS WeatherDb;

-- Use the WeatherDb database
USE WeatherDb;

-- Create the WeatherForecasts table (if it doesn't exist)
CREATE TABLE IF NOT EXISTS WeatherForecasts (
    Id INT AUTO_INCREMENT PRIMARY KEY,  -- Added an ID column as a primary key (best practice)
    Date DATE NOT NULL,
    TemperatureC INT NOT NULL,
    Summary VARCHAR(255)
);
```
