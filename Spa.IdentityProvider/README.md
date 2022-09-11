# Duende IdentityServer

This solution was created after installing templates from
[this repository](https://github.com/DuendeSoftware/IdentityServer.Templates)
using these commands:

```cmd
dotnet new isinmem -o "."
dotnet new gitignore --output "." --force
dotnet new tool-manifest --output "." --force
dotnet new nugetconfig --output "." --force
dotnet new globaljson
dotnet new sln -o "." --force
dotnet sln "." add "."
```
