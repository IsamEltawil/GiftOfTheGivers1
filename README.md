# Gift of the Givers – Disaster Relief Web App

A C# ASP.NET Core web application built for the **Gift of the Givers Foundation**, South Africa's largest African-based disaster relief organisation. The app helps the foundation coordinate relief efforts by letting the public report disasters, donate resources and sign up as volunteers, all in one place.

Developed as a group Portfolio of Evidence for **APPR6312 Applied Programming** at The Independent Institute of Education (IIE), 2026.

[![Build Status](https://dev.azure.com/GiftGiversFoundationProject/Gift%20of%20the%20Giver%20foundation/_apis/build/status/PIPELINE_NAME?branchName=master)](https://dev.azure.com/GiftGiversFoundationProject/Gift%20of%20the%20Giver%20foundation/_build)

---

## Features

- **User registration and login** – secure accounts using ASP.NET Core Identity
- **Disaster incident reporting** – users can report an incident with its location, type, severity and description
- **Resource donations** – record donations of food, clothing, medical supplies and more, and track their status
- **Volunteer management** – volunteers can register, choose tasks and see what they've signed up for
- **Tax certificate service** – an Azure Function that generates a donation tax certificate on request
- **Shared helper library** – reusable validation and formatting helpers packaged as a NuGet package in Azure Artifacts

## Tech stack

- **Language / framework:** C#, ASP.NET Core (.NET 8)
- **Database:** Azure SQL Database with Entity Framework Core
- **Authentication:** ASP.NET Core Identity
- **Serverless:** Azure Functions (HTTP trigger, isolated worker)
- **Hosting:** Azure App Service
- **DevOps:** Azure Boards, Azure Repos, Azure Pipelines, Azure Artifacts
- **Testing:** MSTest

## Project structure

```
GiftOfTheGivers/                 Main web application
GiftOfTheGivers.Functions/       Azure Functions project (tax certificate)
GiftOfTheGivers.Helpers/         Class library published as a NuGet package
GiftOfTheGivers.Tests/           Unit tests
azure-pipelines.yml              CI build pipeline
nuget.config                     Points restore at the Azure Artifacts feed
```

## Getting started

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) with the *ASP.NET and web development* and *Azure development* workloads
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server LocalDB (installed with Visual Studio) or access to the Azure SQL database
- Access to the project's Azure Artifacts feed (for the `GiftOfTheGivers.Helpers` package)

### Run locally

1. Clone the repository:
   ```bash
   git clone https://github.com/IsamEltawil/GiftOfTheGivers1.git
   ```
2. Open the solution (`.sln`) in Visual Studio.
3. Update the connection string in `appsettings.json` (or use User Secrets) to point at your database.
4. Apply the database migrations in the Package Manager Console:
   ```powershell
   Update-Database
   ```
5. Right-click the solution → **Configure Startup Projects** → **Multiple startup projects**, and set both the web app and the Functions project to **Start**.
6. Press **F5**. The web app opens in your browser and the function runs at `http://localhost:7071/api/...`.

> **Note:** Never commit connection strings or function keys. Use User Secrets locally and **App Service → Configuration** in Azure.

### Run the tests

```bash
dotnet test
```

## Continuous integration

Every push to `master` triggers the Azure Pipelines build, which:

1. Restores NuGet packages (including the private Azure Artifacts feed)
2. Builds the solution in Release configuration
3. Runs the unit tests

## Branching workflow

- `master` – stable, always builds
- `feature/<name>` – one branch per feature or team member, merged back into `master` once it builds and passes tests

Write clear commit messages that say what changed, for example `Add validation to donation form` rather than `update`.

## Team

- Isam Eltawil
- Matsobane Junior Lethabo Boshomane
- Mahlatse Mphelo

## Acknowledgements

- [Gift of the Givers Foundation](https://giftofthegivers.org/) – the organisation this project is modelled on
- The Independent Institute of Education – APPR6312 module

---

*This project is an academic exercise and is not officially affiliated with the Gift of the Givers Foundation.*
