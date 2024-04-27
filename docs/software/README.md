# Реалізація інформаційного та програмного забезпечення
  
## SQL-Скрипт для створення початкового наповнення бази даних

```sql
<!-- @include: ./sql/db.sql -->
```

## RESTfull сервіс для управління даними

### ApplicationDbContex для зв'язку з базою даних

```csharp
<!-- @include: ./RestFullServices/Data/ApplicationDbContext.cs -->
```

### Моделі

```csharp
<!-- @include: ./RestFullServices/Models/Users.cs -->
```
Загальна модель користувачів

```csharp
<!-- @include: ./RestFullServices/Models/UserDTO.cs -->
```
Модель для реєстрації

```csharp
<!-- @include: ./RestFullServices/Models/UserLogin.cs -->
```
Модель для авторизації

### Валідація введених даних

```csharp
<!-- @include: ./RestFullServices/Validation/RegisterValidator.cs -->
```

```csharp
<!-- @include: ./RestFullServices/Validation/LoginValidator.cs -->
```

### Конфігураційний файл

```csharp
<!-- @include: ./RestFullServices/appsettings.json -->
```
Цей файл потрібен для звернення до бази через ConnectionStrings

### Основний файл в якому відбуваються всі налаштування API

```csharp
<!-- @include: ./RestFullServices/Program.cs -->
```
