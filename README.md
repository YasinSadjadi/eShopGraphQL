# eShop GraphQL API

This is an **eShop backend project** built with **GraphQL** using [HotChocolate v14](https://chillicream.com/docs/hotchocolate).  
It provides a clean and scalable architecture for managing products, categories, and orders through a GraphQL API.

---

## 📌 Features

- **GraphQL API** built with HotChocolate v14
- **Entity Framework Core** for database access
- **Repository pattern** for data abstraction
- **Dockerized database setup** using `docker compose`
- **Scalable architecture** ready for extension

---

## 📦 Dependencies

This project uses the following NuGet packages:

- `HotChocolate.AspNetCore` (GraphQL server)
- `HotChocolate.Data` (filtering, sorting, paging support)
- `HotChocolate.Data.EntityFramework` (EF integration with GraphQL)
- `HotChocolate.Types` (schema types, object types, etc.)
- `Microsoft.EntityFrameworkCore` (Entity Framework Core ORM)
- `Microsoft.EntityFrameworkCore.Tools` (EF Core CLI tools)
- `Microsoft.Extensions.DependencyInjection` (dependency injection)

---

## 🚀 Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/YasinSadjadi/eShopGraphQL.git
cd eShop-GraphQL
```
### 2. Make project ready to run
```bash
dotnet restore
dotnet build
dotnet ef database update
```
### 3. Start up database
run docker desktop and run the command below in your project directory
```bash
docker compose -f compose.yml up -d
```
### 4. Run application
you can run application using cli or other ways
```bash
dotnet run --project eShop.Catalog.Api
```
### 5. Just test application
go to this url in your browser and use Nitro or BananaCakePop to test application using your queries.
```
http://localhost:5224/graphql/
```
