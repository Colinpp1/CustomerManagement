# Customer Management System

A C# console application demonstrating CRUD operations with Entity Framework Core and SQLite, following clean architecture principles.

## What This Program Does

- **Create** new customers with Name, Email, and Phone Number
- **Read** all customers or get a specific customer by ID
- **Update** existing customer information
- **Delete** customers from the database
- Uses **Entity Framework Core** with **SQLite** database
- Follows **Clean Architecture** with Repository and Service patterns

## Architecture

```
CustomerManagement/
├── Models/                    # Domain entities
│   └── Customer.cs
├── Data/                      # Database context
│   └── CustomerDbContext.cs
├── Repositories/              # Data access layer
│   ├── ICustomerRepository.cs
│   └── CustomerRepository.cs
├── Services/                  # Business logic layer
│   ├── ICustomerService.cs
│   └── CustomerService.cs
└── Program.cs                 # Presentation layer
```

## How to Run

git clone repository
### Command Line
bash
cd file_location
dotnet run


### Visual Studio
1. Open `CustomerManagement.csproj`
2. Press `F5` to run

## Features

✅ **Clean Architecture** - Separation of concerns with layers  
✅ **Repository Pattern** - Abstract data access  
✅ **Service Layer** - Business logic separation  
✅ **Async/Await** - Non-blocking database operations  
✅ **Entity Framework Core** - ORM for database operations  
✅ **SQLite** - Lightweight embedded database  
✅ **Data Validation** - Required fields and email validation  
✅ **Unique Email** - Email index ensures no duplicates  

## Database

- **Database File**: `customers.db` (created automatically)
- **Table**: Customers
- **Columns**:
  - CustomerID (Primary Key, Auto-increment)
  - Name (Required, Max 100 chars)
  - Email (Required, Unique, Max 100 chars)
  - PhoneNumber (Optional, Max 20 chars)

## Sample Usage

```
╔════════════════════════════════════════════╗
║   Customer Management System (EF Core)    ║
╚════════════════════════════════════════════╝

┌────────────────────────────────────┐
│           MAIN MENU                │
├────────────────────────────────────┤
│ 1. List All Customers              │
│ 2. Get Customer by ID              │
│ 3. Create New Customer             │
│ 4. Update Customer                 │
│ 5. Delete Customer                 │
│ 6. Exit                            │
└────────────────────────────────────┘

Enter your choice: 3

═══ CREATE NEW CUSTOMER ═══

Name: Colin
Email: colin@example.com
Phone Number: +27817465178

✓ Customer created successfully with ID: 1
```

## Technologies Used

- **.NET 9.0**
- **Entity Framework Core 9.0**
- **SQLite**
- **C# 12**
- **Async/Await**
