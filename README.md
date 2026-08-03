# Hotel Management System

## 📌 Description

Hotel Management System is a backend Web API project built with ASP.NET Core.
The system is designed to manage hotels, rooms, users, authentication, and bookings.

The main goal of the project is to provide a complete hotel management backend with:
- User authentication
- JWT authorization
- Hotel management
- Room management
- Booking management
- User profile management
- PostgreSQL database integration

---

# 🏗 Project Architecture

The project uses Clean Architecture principles.

```
HotelManagementSystem
│
├── API
│   ├── Controllers
│   ├── Middleware
│   ├── Program.cs
│   └── appsettings.json
│
├── Application
│   ├── DTOs
│   ├── Interfaces
│   └── Services contracts
│
├── Domain
│   ├── Entities
│   └── Base classes
│
└── Infrastructure
    ├── Data
    ├── Services
    ├── DependencyInjection.cs
    └── Database configuration
```

---

# 🛠 Technologies

## Backend

- ASP.NET Core Web API
- .NET 9
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Swagger
- BCrypt Password Hashing

---

# 🗄 Database

Database:
```
PostgreSQL
```

Main tables:

## Users

Stores system users.

Fields:

- Id
- FullName
- Email
- PasswordHash
- RoleId
- IsActive


## Roles

Stores user roles.

Examples:

- Admin
- User


## Hotels

Stores hotel information.

Fields:

- Id
- Name
- Address
- Description


## Rooms

Stores hotel rooms.

Fields:

- Id
- HotelId
- Number
- Type
- Price
- IsAvailable


## Bookings

Stores room reservations.

Fields:

- Id
- UserId
- RoomId
- CheckIn
- CheckOut
- Status


## RefreshTokens

Stores JWT refresh tokens.

---

# 🔐 Authentication

The project uses JWT authentication.

Implemented:

## Register

Endpoint:

```
POST /api/Auth/register
```

Creates a new user.

Example:

```json
{
  "fullName": "Rizo",
  "email": "rizosha@gmail.com",
  "password": "123456"
}
```


## Login

Endpoint:

```
POST /api/Auth/login
```

Returns JWT token after successful authentication.


Authentication flow:

```
User
 |
 | Register
 ↓
Database
 |
 | Login
 ↓
Check password
 |
 ↓
Generate JWT Token
 |
 ↓
Access protected endpoints
```

---

# 👤 User Management

Implemented:

- View profile
- Update profile
- Change password


User profile contains:

- Full name
- Email
- Role
- Account status


---

# 🏨 Hotel Management

Admin can:

- Create hotel
- Get hotels
- Update hotel
- Delete hotel


Example:

```
POST /api/Hotels
GET /api/Hotels
PUT /api/Hotels/{id}
DELETE /api/Hotels/{id}
```

---

# 🚪 Room Management

Admin can:

- Create rooms
- View rooms
- Update rooms
- Delete rooms


Room belongs to a hotel:

```
Hotel
 |
 |
 └── Rooms
```

---

# 📅 Booking Management

Users can:

- Book available rooms
- View bookings


Booking logic:

```
User selects room
        |
        ↓
Check room availability
        |
        ↓
Create booking
        |
        ↓
Room becomes unavailable
```

---

# ⚙️ Dependency Injection

Services are registered using:

```
Infrastructure/DependencyInjection.cs
```

Example:

```csharp
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IJwtService, JwtService>();
services.AddScoped<IHotelService, HotelService>();
services.AddScoped<IRoomService, RoomService>();
services.AddScoped<IBookingService, BookingService>();
```

---

# 🚀 How to Run Project

## 1. Clone repository

```bash
git clone https://github.com/your-name/HotelManagementSystem.git
```

---

## 2. Install packages

```bash
dotnet restore
```

---

## 3. Configure database

Open:

```
API/appsettings.json
```

Set PostgreSQL connection:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=hotel_db;Username=postgres;Password=password"
}
```

---

## 4. Create migrations

Run:

```bash
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project API
```

---

## 5. Update database

```bash
dotnet ef database update --project Infrastructure --startup-project API
```

---

## 6. Run API

```bash
dotnet run --project API
```

---

# 📖 Swagger

Swagger is available at:

```
http://localhost:5000/swagger
```

Swagger provides:

- API documentation
- Testing endpoints
- JWT authorization


---

# 🔑 Authorization in Swagger

1. Login using:

```
POST /api/Auth/login
```

2. Copy received token.

3. Click:

```
Authorize 🔒
```

4. Insert:

```
Bearer YOUR_TOKEN
```

Example:

```
Bearer eyJhbGciOiJIUzI1NiIs...
```

---

# 👨‍💻 Team

Project developed as a team project.

Responsibilities:

- Authentication
- Users
- Hotels
- Rooms
- Booking system
- Database integration


---

# ✅ Project Status

Completed backend system with:

✔ Clean Architecture  
✔ PostgreSQL Database  
✔ Entity Framework Core  
✔ JWT Authentication  
✔ CRUD Operations  
✔ Swagger Documentation  



















# Hotel Management System

## 📌 Описание проекта

**Hotel Management System** — это Backend Web API система для управления гостиницей.

Проект разработан на **ASP.NET Core Web API** и предназначен для автоматизации работы гостиницы:

- управление пользователями
- регистрация и авторизация
- управление отелями
- управление комнатами
- бронирование комнат
- управление профилем пользователя

Главная цель проекта — создать полноценную серверную часть гостиничной системы с использованием современных технологий .NET.

---

# 🏗 Архитектура проекта

Проект построен с использованием принципов **Clean Architecture**.

Структура:

```
HotelManagementSystem
│
├── API
│   ├── Controllers
│   ├── Middleware
│   ├── Program.cs
│   └── appsettings.json
│
├── Application
│   ├── DTOs
│   ├── Interfaces
│   └── Бизнес-логика
│
├── Domain
│   ├── Entities
│   └── Основные модели
│
└── Infrastructure
    ├── Data
    ├── Services
    ├── Entity Framework Core
    └── Dependency Injection
```

---

# 🛠 Используемые технологии

## Backend:

- ASP.NET Core Web API
- .NET 9
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- Swagger
- BCrypt

---

# 🗄 База данных

Используется:

```
PostgreSQL
```

Основные таблицы:

---

## Users (Пользователи)

Хранит информацию о пользователях системы.

Поля:

- Id
- FullName
- Email
- PasswordHash
- RoleId
- IsActive


Пример:

```
User
 |
 └── Role
```

---

## Roles (Роли)

Определяет права пользователя.

Примеры:

```
Admin
User
```

---

## Hotels (Отели)

Хранит информацию об отелях.

Поля:

- Id
- Name
- Address
- Description


---

## Rooms (Комнаты)

Хранит информацию о комнатах.

Поля:

- Id
- HotelId
- Number
- Type
- Price
- IsAvailable


Связь:

```
Hotel
 |
 └── Rooms
```

---

## Bookings (Бронирования)

Хранит бронирования пользователей.

Поля:

- Id
- UserId
- RoomId
- CheckIn
- CheckOut
- Status


Связь:

```
User
 |
 └── Booking
        |
        └── Room
```

---

## RefreshTokens

Хранит токены обновления JWT.

Используется для безопасной авторизации пользователя.

---

# 🔐 Система авторизации

В проекте реализована JWT Authentication.

Есть:

- Регистрация
- Авторизация
- Проверка пароля
- Создание JWT Token
- Защищенные API методы


---

# 📝 Регистрация пользователя

Endpoint:

```
POST /api/Auth/register
```

Создает нового пользователя.


Пример запроса:

```json
{
  "fullName": "Rizo",
  "email": "rizosha@gmail.com",
  "password": "123456"
}
```

Пароль сохраняется в базе данных в зашифрованном виде:

```
Password
     |
     ↓
BCrypt Hash
     |
     ↓
PasswordHash
```

---

# 🔑 Авторизация пользователя

Endpoint:

```
POST /api/Auth/login
```

Процесс:

```
Пользователь вводит Email и Password

          ↓

Проверка пользователя в БД

          ↓

Проверка пароля BCrypt

          ↓

Создание JWT Token

          ↓

Доступ к защищенным API
```

---

# 👤 Управление пользователем

Реализовано:

- просмотр профиля
- изменение профиля
- изменение пароля


Пользователь может изменять:

- FullName
- Email
- Password

---

# 🏨 Управление отелями

Администратор может:

- создать отель
- получить список отелей
- изменить данные отеля
- удалить отель


CRUD операции:

```
POST
GET
PUT
DELETE
```

---

# 🚪 Управление комнатами

Администратор может:

- добавлять комнаты
- получать комнаты
- изменять комнаты
- удалять комнаты


Комната принадлежит определенному отелю:

```
Hotel

 |
 ↓

Room 1
Room 2
Room 3
```

---

# 📅 Система бронирования

Пользователь может забронировать комнату.


Логика:

```
Выбор комнаты

      ↓

Проверка доступности

      ↓

Создание Booking

      ↓

Комната становится занятой
```

---

# ⚙️ Dependency Injection

Все сервисы подключаются через:

```
Infrastructure/DependencyInjection.cs
```


Пример:

```csharp
services.AddScoped<IAuthService, AuthService>();

services.AddScoped<IJwtService, JwtService>();

services.AddScoped<IHotelService, HotelService>();

services.AddScoped<IRoomService, RoomService>();

services.AddScoped<IBookingService, BookingService>();
```

---

# 🚀 Запуск проекта

## 1. Установка зависимостей

```bash
dotnet restore
```


---

## 2. Настройка базы данных

Открыть:

```
API/appsettings.json
```

Настроить:

```json
"ConnectionStrings": {
  "DefaultConnection":
  "Host=localhost;Database=hotel_db;Username=postgres;Password=password"
}
```

---

## 3. Создание миграций

```bash
dotnet ef migrations add InitialCreate --project Infrastructure --startup-project API
```


---

## 4. Обновление базы данных

```bash
dotnet ef database update --project Infrastructure --startup-project API
```


---

## 5. Запуск API

```bash
dotnet run --project API
```

---

# 📖 Swagger

После запуска открыть:

```
http://localhost:5000/swagger
```


Swagger позволяет:

- смотреть все API методы
- отправлять запросы
- тестировать авторизацию

---

# 🔒 JWT в Swagger

Чтобы использовать защищенные методы:

1. Сделать Login:

```
POST /api/Auth/login
```

2. Скопировать Token.

3. Нажать:

```
Authorize 🔒
```

4. Вставить:

```
Bearer TOKEN
```


Пример:

```
Bearer eyJhbGciOiJIUzI1NiIs...
```

---

# 👨‍💻 Командная работа

Проект разработан командой.

Основные части:

- Authentication
- Users
- Hotels
- Rooms
- Bookings
- Database
- API Controllers



---

# ✅ Статус проекта

Проект содержит:

✔ Clean Architecture  
✔ PostgreSQL Database  
✔ Entity Framework Core  
✔ JWT Authentication  
✔ CRUD операции  
✔ Swagger документацию  
✔ Dependency Injection  
