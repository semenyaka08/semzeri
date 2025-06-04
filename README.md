# Semzeri

Semzeri is a Backend URL shortening service built with **C#**, **ASP.NET Core**, and **Entity Framework Core**. This application allows users to shorten long URLs into shorter, more manageable links.

## Features

- **URL Shortening**: Convert long URLs into short, easy-to-remember links.
- **Redirection**: Automatically redirect users to the original URL when they visit a short link.
- **Persistence**: Data stored in a database using Entity Framework Core.

## Technologies Used

- **Backend**: ASP.NET Core with Entity Framework Core for database management.
- **Database**: Microsoft SQL Server.

# Getting Started

## Prerequisites

Ensure the following are installed on your system:

- .NET SDK (6.0 or later)
- Docker and Docker Compose
- Postman (for API testing)

## Running the Application

### 1. Clone the repository

```bash
git clone <repository-url>
cd Semzeri
```

### 2. Start the application with Docker Compose

```bash
cd backend
docker-compose -f compose.yaml up -d
```

This will start all required services including the application and SQL Server database.

### 3. Verify the application is running

The API will be available at `http://localhost:6010` (or the port specified in your compose file).

## API Interaction

### Using Postman

#### Import the Postman Collection

1. Open Postman
2. Click "Import" button
3. Select the `semzeri.postman.collection.json` file from the project root
4. The collection will contain all available API endpoints with pre-configured requests

### Admin Access

If you need admin privileges for testing, use these credentials:

```json
{
  "Email": "Admin@gmail.com",
  "Password": "Pa$$w0rd"
}
```

### Available Endpoints

The Postman collection includes endpoints for:

- Creating short URLs
- Retrieving original URLs
- Managing user accounts
- Admin operations

## Stopping the Application

```bash
docker-compose -f compose.yaml down
```
