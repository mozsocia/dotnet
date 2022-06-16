
```
dotnet new console -o app1


dotnet new classlib -o app2

cd .\app1\
 
 
dotnet add reference ..\app2\app2.csproj

```

#### another way of adding reference
```
dotnet new console -o app3

dotnet add .\app1\app1.csproj reference .\app3\app3.csproj

```

#### another way of adding multiple reference
```
dotnet add cAppl/cApp1.csproj reference c13/c13.csproj c14/c14.csproj

```

use add_ref_project.zip
