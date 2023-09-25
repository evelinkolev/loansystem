
<h1 align="center">Loan System</h1>
<p align="center"><i>Payments solution implementing clean architecture & jwt bearer authentication in asp.net 7 using MediatR & Mapster.</i></p>
<div align="center">
  <a href="https://github.com/evelinkolev/loansystem/stargazers"><img src="https://img.shields.io/github/stars/evelinkolev/loansystem" alt="Stars Badge"/></a>
<a href="https://github.com/evelinkolev/loansystem/network/members"><img src="https://img.shields.io/github/forks/evelinkolev/loansystem" alt="Forks Badge"/></a>
<a href="https://github.com/evelinkolev/loansystem/pulls"><img src="https://img.shields.io/github/issues-pr/evelinkolev/loansystem" alt="Pull Requests Badge"/></a>
<a href="https://github.com/evelinkolev/loansystem/issues"><img src="https://img.shields.io/github/issues/evelinkolev/loansystem" alt="Issues Badge"/></a>
<a href="https://github.com/evelinkolev/loansystem/graphs/contributors"><img alt="GitHub contributors" src="https://img.shields.io/github/contributors/evelinkolev/loansystem?color=2b9348"></a>
<a href="https://github.com/evelinkolev/loansystem/blob/master/LICENSE"><img src="https://img.shields.io/github/license/evelinkolev/loansystem?color=2b9348" alt="License Badge"/></a>
</div>
<br>
<p align="center"><i>If you are intersted, please feel free to educate me on any topic if you have any tips I'd love to hear from you and learn from you as well.</i></p>
<br>

To create a Payer object for a User, you will send a POST request with a request body represented in the sample below*. Since, just like in the real world, a customer may have a number of accounts from which to make payments, Users can have multiple Payers. The PayerId that is returned with a successful API call can be used to specify the Card when making a payment.

```js
POST {{host}}/api/v1/payers
```

#### Payer Request
```json
{
    "fullName": "Test Testov",
    "deposit": 914273
}
```

```js
200 OK
```

#### Payer Response
```json
{
    "id": "9651ad1d-bb64-4872-ba61-08db85d82f7f",
    "fullName": "Test Testov",
    "deposit": 914273,
    "routingNumber": "45118840",
    "accountNumber": "259087750",
    "createdDateTime": "2023-09-16T06:48:44.3411836Z",
    "updatedDateTime": "0001-01-01T00:00:00"
    "userId": "0fe4f18d-7784-4e1f-973b-5743cgc7dca"
}
```

## Documentation

### [Getting started](https://github.com/evelinkolev/loansystem/wiki/Getting%E2%80%90started)
>**This page contains important information on how to properly configure LoanSystem.Api**

## Database

The project is configured to use SQL Server by default.

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

Running database migrations is easy. Ensure you add the following flags to your command (values assume you are executing from repository root)

* `--project LoanSystem.Infrastructure` (optional if in this folder)
* `--startup-project LoanSystem.Api`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project LoanSystem.Infrastructure --startup-project LoanSystem.Api`

## Technologies

* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 7](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [Mapster](https://github.com/MapsterMapper/Mapster)

## Versions
The main branch is now on .NET 7.0. The following previous versions are available:

* [7.0](https://github.com/evelinkolev/loansystem/tree/master)

## Support

If you are having problems, please let me know by [raising a new issue](https://github.com/evelinkolev/loansystem/issues/new/choose).

## License

This project is licensed with the [MIT license](LICENSE.txt).
