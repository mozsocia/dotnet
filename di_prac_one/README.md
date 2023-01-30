### chatgpt ans

---
created: 2023-01-30T23:25:44 (UTC +06:00)
tags: []
source: https://chat.openai.com/chat/341d4791-8526-434c-a474-dfc0b8762bf4
author: 
---

# "DI in .Net Core Console"

> ## Excerpt
> A conversational AI system that listens, learns, and challenges

---
how to setup up Dependency injection in dotnet core console app

To set up dependency injection in a .NET Core console application, you can use the built-in dependency injection (DI) framework. Here are the basic steps:

1.  Install the Microsoft.Extensions.DependencyInjection package using NuGet:

```
dotnet add package Microsoft.Extensions.DependencyInjection

```

2.  In the `Main` method of the console application, create a new `ServiceCollection` object and configure the services that will be used in the application:

```
var services = new ServiceCollection();
services.AddTransient<IMyService, MyService>();

```

3.  Build the service provider:

```
var serviceProvider = services.BuildServiceProvider();

```

4.  Use the `GetService` method to resolve dependencies in your application:

```
var myService = serviceProvider.GetService<IMyService>();

```

You can also use built-in DI container and use `AddScoped`, `AddTransient` and `AddSingleton` method to register your services accordingly.

You can find more detailed information about how to use the dependency injection framework in the Microsoft documentation: [https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1)

![](data:image/svg+xml,%3csvg%20xmlns=%27http://www.w3.org/2000/svg%27%20version=%271.1%27%20width=%2730%27%20height=%2730%27/%3e)![alvin smith](https://chat.openai.com/_next/image?url=https%3A%2F%2Flh3.googleusercontent.com%2Fa%2FAEdFTp6-KnLeV6yIkitOU1UG9VoU2r7pf_GWC3w5F5oF%3Ds96-c&w=64&q=75)

give me full code with file name and folder structure

Sure, here's an example of a .NET Core console application that uses dependency injection to inject an interface called `IMyService` into the `Program` class.

This is the suggested folder structure:

```
|- MyApp
    |- MyApp.csproj
    |- Program.cs
    |- Services
        |- IMyService.cs
        |- MyService.cs

```

This is the content of the files:

MyApp.csproj

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp3.1</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Extensions.DependencyInjection" Version="3.1.6" />
  </ItemGroup>

</Project>

```

Program.cs

```
using Microsoft.Extensions.DependencyInjection;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddTransient<IMyService, MyService>();

            var serviceProvider = services.BuildServiceProvider();

            var myService = serviceProvider.GetService<IMyService>();
            myService.DoWork();
        }
    }
}

```

IMyService.cs

```
namespace MyApp.Services
{
    public interface IMyService
    {
        void DoWork();
    }
}

```

MyService.cs

```
using MyApp.Services;

namespace MyApp
{
    public class MyService : IMyService
    {
        public void DoWork()
        {
            // Implementation of the service
        }
    }
}

```

In this example, I have used `AddTransient` method to register the `MyService` class as an implementation of the `IMyService` interface. The `BuildServiceProvider` method is then used to create an instance of the service provider, which is used to resolve dependencies in the application. You can use `AddScoped`, `AddTransient` and `AddSingleton` method to register your services accordingly based on the requirement of your application.

You can run this application by using the following command:

```
dotnet run

```

You can also add more services and interfaces to this project, and use dependency injection to inject them into other classes as needed.
