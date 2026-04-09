# SteelPlant MES System Prototype

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Language: C#](https://img.shields.io/badge/Language-C%23-239120.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Framework: .NET 10](https://img.shields.io/badge/Framework-.NET%2010-512bd4.svg)](https://dotnet.microsoft.com/download)
[![Database: SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red.svg)](https://www.microsoft.com/en-us/sql-server/)

A lightweight Manufacturing Execution System (MES) prototype designed for a steel plant to manage production batches and grades. This project demonstrates a clean separation between a database layer (SSDT) and a functional .NET console application.

##  ⚡ Features
- **Production Management**: Start new batches with automated logging and finish them with final weight recording.
- **Database-First Approach**: Uses a SQL Server Database Project (.sqlproj) for schema management and deployment.
- **Data Integrity**: Implements SQL Transactions and Stored Procedures to ensure data consistency.
- **Automated Seeding**: Post-deployment scripts to populate initial steel grades (e.g., S235JR, S355J2).

## 🛠 Tech Stack
- **Language**: C# 14 / .NET 10
- **Database**: MS SQL Server (LocalDB)
- **Data Access**: ADO.NET (`Microsoft.Data.SqlClient`)
- **Tools**: Visual Studio 2026, SQL Server Data Tools (SSDT)

## 📂 Project Structure

```
 src/
├── SteelPlant.ConsoleApp/                # Business logic & User Interface
│   ├── Models/                           # Data Transfer Objects (SteelBatch, SteelGrade)
│   ├── DatabaseService.cs                # ADO.NET Data Access Layer
│   ├── Program.cs                        # Application Entry Point
│   └── SteelPlant.ConsoleApp.csproj
└── SteelPlant.Database/                  # Database Schema & Logic (SSDT)
   ├── ProductionLogs.sql                # Table for system logging
   ├── Script.PostDeployment.sql         # Initial data seeding
   ├── SteelBatches.sql                  # Main production data table
   ├── SteelGrades.sql                   # Reference table for steel types
   ├── SteelPlant.Database.sqlproj
   └── sp_FinishBatch.sql                # Stored Procedure for batch completion
```

- **SteelPlant.Database**: A SQL Server Database Project that manages the schema. It allows for version-controlled database development and easy deployment via dacpac.
- **SteelPlant.ConsoleApp**: A .NET 10 application that interacts with the database.
  - `DatabaseService.cs`: Encapsulates all SQL commands, including transaction management and stored procedure calls.
  - `Models/`: Contains clean C# classes that map to database records.
 
## ⚙️ Logic Highlights
- **Transactions**: The `StartNewBatch` method uses `SqlTransaction` to ensure that a batch is only created if the initial production log is successfully written.
- **Stored Procedures**: High-performance batch completion logic is handled on the SQL side via `sp_FinishBatch`.
- **Modern C#**: Utilizes file-scoped namespaces and primary constructor-like logic for clean, readable code.

## 🚀 Getting Started

**Prerequisites**
  - **.NET 10 SDK**
  - **SQL Server Express LocalDB** (or any MS SQL Server instance)

**Installation & Setup**

1. **Clone the repository**:
```bash
git clone https://github.com/AnastasiaZAYU/steel-plant-data-service.git
```
2. **Deploy the Database**:
   - Open `SteelPlantDataService.slnx` in **[Visual Studio](https://visualstudio.microsoft.com/)**.
   - Right-click on `SteelPlant.Database` project and select **Publish**.
   - Target your `(localdb)\MSSQLLocalDB` and name the database `SteelPlantDB`.
3. Run the App:
   - Set `SteelPlant.ConsoleApp` as the Startup Project.
   - Press `F5` or `dotnet run`.

## 📈 Future Improvements
- **Unit Testing**: Add a testing project using `xUnit` or `NUnit` to cover `DatabaseService` logic with mock data.
- **Configuration**: Move connection strings to an external `appsettings.json` file for better security and flexibility.
- **Validation Layer**: Implement input validation to ensure data integrity before sending it to the database.
- **ORM Integration**: Implement an alternative data access layer using **Entity Framework Core** to simplify object-relational mapping.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
