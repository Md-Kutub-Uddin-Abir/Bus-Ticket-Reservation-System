 Bus Ticket Booking System

A full-stack web application for searching, booking, and managing bus tickets.
Built with ASP.NET Core Web API, Angular (Standalone Components), and PostgreSQL (EF Core).

Project Structure

BusTicket/
│
├── Backend/
│   ├── Application/
│   ├── Domain/
│   ├── Infrastructure/
│   ├── WebApi/
│   └── Test/
│
├── Frontend/
│   └── src/
│       └── app/
│           ├── components/
│           │   ├── search-bus/
│           │   └── seat-view/
│           └── assets/
│
└── README.md

Backend Setup (ASP.NET Core 8 + PostgreSQL)
1. Prerequisites:

    i. .NET SDK 8.0

    ii. PostgreSQL 15+

    iii. Visual Studio Code or Visual Studio

2️⃣ Configure Connection String
Edit file:
Backend/WebApi/appsettings.json as the code given below:

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=BusTicketDb;Username=postgres;Password=1234"
}

3️⃣ Run Migrations:
cd Backend
dotnet ef database update

4️⃣ Run the API
cd WebApi
dotnet run


Frontend Setup (Angular 18 Standalone)
1️⃣ Prerequisites
    Node.js 18+
    Angular CLI

2️⃣ Install Dependencies
    cd Frontend
    npm install
3️⃣ Run Angular App

Features:

    Search buses by route and date

    View available buses and seat layout

    Book seats (real-time update)

    Auto seat generation per schedule

    Validation and error handling

    Clean Architecture (Domain, Application, Infrastructure, WebApi)

