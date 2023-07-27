
# Boilerplate .NET API

### In progress

Created to be used as a reference for new projects


## Stack


![Stack](https://github-readme-tech-stack.vercel.app/api/cards?title=Stack&lineCount=1&bg=%230D1117&badge=%23161B22&border=%2321262D&titleColor=%2358A6FF&line1=redis%2CRedis%2C6ab655%3Bmongodb%2CMongo+DB%2C28b768%3Brabbitmq%2CRabbit+MQ%2C4707d8%3B)

## Attention

This project is organized using SOLID and DDD principles (of course it does not meet all the principles).

if you are going to use it, read the code and see if it fits



## Features

- CRUD Company
- Redis Integration
- MassTransit for MessageQueue
- MongoDB Repository
- Versioning API

## Roadmap

- Finish CRUD Company
    - Create
    - Read
    - Update
    - Remove
    - List All
- Alter class MassTransit to Generic class
- Create Filter and Pagination


# Project Structure

* [domain/](./src/domain)
  * [Application/](./src/domain/Application)
  * [Entities/](./src/domain/Entities)
* [infra/](./src/infra)
  * [Data/](./src/infra/Data)
  * [Dto/](./src/infra/Dto)
  * [Resolver/](./src/infra/Resolver)
* [integrations/](./src/integrations)
  * [MessageQueue/](./src/integrations/MessageQueue)
  * [Redis/](./src/integrations/Redis)
* [presentation/](./src/presentation)
  * [Api/](./src/presentation/Api)
* [shared/](./src/shared)
  * [Base/](./src/shared/Base)
  * [Consts/](./src/shared/Consts)
  * [Utils/](./src/shared/Utils)
* [test/](./src/test)
  * [DomainApplicationTest/](./src/test/DomainApplicationTest)

## Running

### Pre requisites


- Visual Studio or Rider*
- Mongo DB*
- .NET Core 7*
- Redis
- RabbitMQ, SQS, Azure Service Bus

Open project in Visual studio or Rider, if necessary run to restore packages

```bash
    dotnet restore
```

configure file `lauchsettings.json` in `src/presentation/Api/Properties/lauchsettings.json`

```json
 "environmentVariables": {
        "MONGO_CONNECTION_STRING": "mongodb://developer:%40Developer@192.168.0.1/?authMechanism=SCRAM-SHA-1&authSource=admin",
        "REDIS_CONNECTION_STRING": "localhost:8080",
        "ASPNETCORE_ENVIRONMENT": "Development"
}
```

after this configure Constants in `src/shared/Consts/LaunchSettings.cs`

```csharp
public static class LaunchSettings
{
    #region Mongo DB
    public static string ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? "SET_HERE_LOCAL_DEVELOPMENT";
    public static string Database = "BOILERPLATEPROJECT";
    #endregion

    #region Redis
    public static string RedisConnectionString = Environment.GetEnvironmentVariable("REDIS_CONNECTION_STRING") ?? "SET_HERE_LOCAL_DEVELOPMENT";
    public static string[] RedisDatabases = {"BOILERPLATEPROJECT"};
    public static double KeepAlive = 10;
    public static double ResponseTimeout = 10;
    public static double ConnectTimeout = 10;
    public static string KeyPrefix = "KEY_BOILERPLATE";
    
    #endregion
}
```

and running project in `src/presentation/Api/Api.csproj` with command

```bash
    dotnet run
```

Default URL: http://localhost:5274/swagger/index.html
## API Reference

## Version: v1

**Contact information:**  
Hugo Guedes  
https://github.com/hrguedes/boilerplate-net-api  
hugo.guedes@hrguedes.dev  

### /api/v1/Company/add

#### POST
##### Summary:

Add new Company

##### Description:

POST /api/v1/Company/add

##### Responses

| Code | Description |
| ---- | ----------- |
| 200 | Company Created |
| 400 | Client Error |
| 500 | Server Error (Contact Admin) |


## Authors

- [@hrguedes](https://github.com/hrguedes)


## References

 - [C#](https://learn.microsoft.com/pt-br/dotnet/csharp/)
 - [SOLID](https://pt.wikipedia.org/wiki/SOLID)
 - [MONGODB](https://www.mongodb.com/docs/)
 - [MONGODB C# DRIVER](https://www.mongodb.com/docs/drivers/csharp/current/)
 - [DDD](https://en.wikipedia.org/wiki/Domain-driven_design)
 - [DDD C#](https://learn.microsoft.com/en-us/archive/msdn-magazine/2009/february/best-practice-an-introduction-to-domain-driven-design)
 
