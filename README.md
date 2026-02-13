# LibraryService

.NET Web API пример по шаблон (N-tier архитектура) с:
- **MongoDB** storage
- **Serilog**
- **Dependency Injection**
- **IConfiguration / IOptionsMonitor** (MongoDbConfiguration)
- **Swagger**
- **HealthCheck** (Mongo ping)
- **Mapper** (Mapster)
- **FluentValidation**
- **Unit tests** (xUnit + Moq)

## Тема
Мини библиотечна система: **книги** и **членове**.

- CRUD контролер: `BooksController`
- Бизнес контролер: `BorrowController` (взимане на книга)

## Как да стартираш
1. Пусни локално MongoDB (примерно с Docker):
   ```bash
   docker run -d --name mongo -p 27017:27017 mongo:7
   ```
2. Отвори `appsettings.json` и провери секцията `MongoDbConfiguration`.
3. Стартирай `LibraryService.Host` проекта.

HealthCheck: `GET /healthz`

Swagger: `/swagger`

## GitHub
Качи цялата папка `LibraryService-main` като репо.
