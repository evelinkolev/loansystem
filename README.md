

# LoanSystem

<p align='left'><strong>LoanSystem</strong> is an open-source RESTful API with authentication using JSON Web Token made in C# and the Entity Framework Core.</p>

## Documentation

<p align='left'>LoanSystem consists of one base component - the loan server listening for requests.</p>

## Setup

<p align='left'>Before you start working, make sure you download all the dependencies (packages) required for each technology and set up the databases! Below are instructions on how to do this:
<br>

The C# project will automatically resolve its <strong>NuGet</strong> dependencies using the NuGet package restore when the project is built.</p>

## LoanSystem.Api

<p align='left'>The LoanSystem.Api can be found in the LoanSystem solution directory. Its purpose is to send back the data of the resource and to receive payload that came in the request.</p>

## Configuration
<p align='left'>To configure the LoanSystem.Api, please follow the steps below:</p>

### Switching connection string depending on environment
<p align='left'>The connection string can be found in the <samp>LoanSystem/LoanSystem.Api/appsettings.json</samp> file.</p>

Example:
```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=loan-system;Trusted_Connection=True;TrustServerCertificate=True;"
  }
```

<p align='left'>First you change connection string, then run the LoanSystem.Api so that the database gets created.</p>
