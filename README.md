# Employee Management System

This Employee Management System is a web application with both API and UI components, designed to manage employees and departments. It supports features like employee and department management, searching, pagination, and JWT-based authentication for secure API access.

## Features
- **Employee Management:** Add, edit, delete, and view employees.
- **Department Management:** Add, edit, delete, and view departments.
- **Search Functionality:** Search employees by name, department, or hire date.
- **Pagination:** Paginate employee results for efficient data handling.
- **Authentication:** Secure API endpoints with JWT-based authentication.

## Project Structure
- **EmployeeMgmt.API:** Contains the API controllers and endpoints.
- **EmployeeMgmt.Web:** Contains the web-based front end.
- **EmployeeMgmt.Application:** Handles business logic services and DTOs.
- **EmployeeMgmt.Infrastructure:** Contains data repositories, EF Core migrations, and data access configuration.

## Design Choices
- **Layered Architecture:** The project is split into layers (API, Web, Application, Infrastructure) to separate concerns and enhance maintainability.
- **Entity Framework Core:** Used for database management, ensuring seamless ORM functionality and database migrations.
- **JWT Authentication:** Secures API endpoints with a JSON Web Token for robust security.
- **Pagination and Search:** Implemented to handle large datasets and improve user experience.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/)
- [PostgreSQL](https://www.postgresql.org/download/) (or a different database if configured)
- [Redis (optional)](https://redis.io/download) - if caching is enabled
- [Node.js & npm](https://nodejs.org/en/download/) (if any frontend tooling is used)

## Setup Instructions

### Step 1: Clone the Repository
```bash
git clone <repository-url>
cd EmployeeMgmt
```

### Step 2: Install Dependencies
```bash
dotnet restore
```


### Step 3: Configure DB Connection String
# Open the appsettings.json file in EmployeeMgmt.Web 
# Replace the DefaultConnection string with your PostgreSQL connection string
# For example, if your PostgreSQL server is running on localhost and the database name is employeemgmt, the connection string would be:
```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=employeemgmt;Username=your_username;Password=your_password"
  },
```
# Similarly, replace the DefaultConnection string in EmployeeMgmt.API/appsettings.json



### Step 3: Run DB Migrations
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Running the Application

To run the web application, navigate to the EmployeeMgmt.Web directory and run the following command:

```bash
dotnet run
```

To Run the API, navigate to the EmployeeMgmt.API directory and run the following command:

```bash
dotnet run
```

## License
This project is licensed under the MIT License.



