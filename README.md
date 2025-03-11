Phonebook System

📌 Overview

This project is a Phonebook System consisting of a .NET Core API and an Angular 16 front-end, designed using Onion Architecture with PostgreSQL as the database. The system allows users to:

View a list of contacts stored in the database.

Add new contacts (Name, Phone Number, and Email).

Remove contacts.

Search for contacts by any part of the name, phone number, or email.

🏗 Architecture & Tech Stack

Back-End (ASP.NET Core + PostgreSQL)

Onion Architecture for maintainability & separation of concerns.

Repository Pattern for data access.

Fluent Validation for request validation.

DTOs (Data Transfer Objects) to structure responses.

AutoMapper for object mapping.

Dependency Injection for better modularity.

ResultView (ApiResponse) for standardized API responses.

Pagination for efficient data handling.

Non-clustered index to optimize search performance.

Front-End (Angular 16)

Simple and intuitive UI to manage contacts.

Search functionality to find contacts easily.

Modern design principles for a smooth user experience.

🚀 Getting Started

Prerequisites

.NET 7+ installed (Download .NET)

Node.js & npm installed (Download Node.js)

PostgreSQL installed and running (Download PostgreSQL)

Database Setup

Create a PostgreSQL database.

Update the connection string in appsettings.json.

Run EF Core migrations:

dotnet ef database update

Running the API

cd PhoneBookSystemAPI
 dotnet run

The API will be available at: http://localhost:5000

Running the Front-End

cd PhoneBookSystemFront
npm install
ng serve

The app will be available at: http://localhost:4200

📷 Screenshots

(Attach UI screenshots if needed)

📞 Contact

For any questions, feel free to reach out.

✅ Developed by: Ahmed Maklad

