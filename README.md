# ClickHouse.Net.Migrations
Basic migrations functionality for ClickHouse.Ado

## Install

### via Package Manager
```powershell
PM> Install-Package ClickHouse.Net.Migrations
```

### via dotnet CLI
```
> dotnet add package ClickHouse.Net.Migrations
```

## Use

In your `Startup.cs` add to `ConfigureServices`:

```c#
services.AddClickHouseMigrations();
```

and define how to resolve `ClickHouseConnectionSettings`:

```c#
services.AddTransient(p => new ClickHouseMigrationSettings(..));
```

Then add `IClickHouseMigrations` as dependency in any of your classes.