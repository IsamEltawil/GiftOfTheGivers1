# Gift of the Givers – Disaster Relief Web App

[![Build Status](https://dev.azure.com/ST10452214/DoomsDay/_apis/build/status/DoomsDay?branchName=master)](https://dev.azure.com/ST10452214/DoomsDay/_build)

An ASP.NET Core Razor Pages web application built for the **Gift of the Givers Foundation**, South Africa's largest African-based disaster relief organisation. The app helps the foundation coordinate relief work by letting people donate resources and receive a donation tax certificate, backed by an Azure Function, a shared helper library and an automated CI pipeline.

Developed as a group Portfolio of Evidence for **APPR6312 Applied Programming** at The Independent Institute of Education (IIE), 2026.

---

## Features

- **Donations** – users can submit a donation through the Donate page
- **Donation tax certificate** – the Certificate page calls an HTTP-triggered Azure Function that generates a tax certificate for a donation
- **Shared helper library** – reusable validation and formatting helpers in `GiftOfTheGivers.Helpers`, published as a NuGet package to Azure Artifacts
- **Unit tests** – MSTest project covering the donation validation helpers
- **Continuous integration** – every push to `master` is restored, built and tested automatically in Azure Pipelines

## Tech stack

| Area | Technology |
|---|---|
| Language / framework | C#, ASP.NET Core Razor Pages (.NET 8) |
| Serverless | Azure Functions (.NET 8 isolated worker, HTTP trigger) |
| Shared code | .NET class library packaged with NuGet |
| Testing | MSTest |
| Source control | Azure Repos (Git), mirrored to GitHub |
| CI | Azure Pipelines (YAML) on a self-hosted agent |
| Packages | Azure Artifacts feed |
| Hosting | Azure App Service / Azure Function App |

## Solution structure

```
GiftOfTheGivers1/               ASP.NET Core Razor Pages web app (Donate, Certificate pages)
GiftOfTheGivers.Functions/      Azure Functions project (donation certificate function)
GiftOfTheGivers.Helpers/        Class library, published as a NuGet package to Azure Artifacts
GiftOfTheGivers.Tests/          MSTest unit tests
.github/workflows/              GitHub Actions workflows used to deploy the Function App
azure-pipelines.yml             Azure Pipelines CI definition (restore, build, test)
nuget.config                    Adds the Azure Artifacts feed for package restore
GiftOfTheGivers1.sln            Visual Studio solution
```

## Getting started

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) with the **ASP.NET and web development** and **Azure development** workloads
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Access to the DoomsDay Azure DevOps project and its Azure Artifacts feed

### Clone the repository

In Visual Studio: **Git → Clone Repository**, then paste:

```
https://ST10452214@dev.azure.com/ST10452214/DoomsDay/_git/DoomsDay
```

### Run locally

1. Open `GiftOfTheGivers1.sln` in Visual Studio.
2. Restore packages (**right-click the solution → Restore NuGet Packages**). Sign in to Azure DevOps if prompted, so the Azure Artifacts feed can be reached.
3. Right-click the solution → **Configure Startup Projects** → **Multiple startup projects**, and set **GiftOfTheGivers1** and **GiftOfTheGivers.Functions** to **Start**.
4. Press **F5**. The web app opens in the browser and the function runs locally at `http://localhost:7071/api/...`.

> **Note:** Never commit connection strings or function keys. Keep them in User Secrets or `local.settings.json` locally, and in **App Service → Configuration** in Azure.

### Run the tests

In Visual Studio: **Test → Test Explorer → Run All**, or from a terminal:

```bash
dotnet test
```

## Continuous integration

The pipeline is defined in `azure-pipelines.yml` and runs in the **DoomsDay** Azure DevOps project on a **self-hosted agent** in the `Default` pool.

Every push to `master` automatically triggers a build that:

1. **Restores** NuGet packages, including the private Azure Artifacts feed (via `nuget.config`)
2. **Builds** all projects in `Release` configuration
3. **Runs** the unit tests in `GiftOfTheGivers.Tests`

Runs started by a push show as **"Individual CI"** in the pipeline history.

## Branching workflow

- `master` – stable code; every push is built and tested by the pipeline
- `feature/<description>` – one branch per feature, e.g. `feature/donation-validation`

Work is done on a feature branch, committed and pushed from Visual Studio, then merged back into `master` with Visual Studio's Git tools (**Git → Manage Branches → Merge into master**).

Commit messages should say what changed, e.g. `Add unit tests for donation amount validation` rather than `update`.

## Team

| Name | GitHub / Azure DevOps |
|---|---|
| Isam Eltawil | IsamEltawil |
| Matsobane Junior Lethabo Boshomane | Matsobane2004 |
| Mahlaste Mphelo | |

## Acknowledgements

- [Gift of the Givers Foundation](https://giftofthegivers.org/) – the organisation this project is modelled on
- The Independent Institute of Education – APPR6312 Applied Programming

---

*This project is an academic exercise and is not officially affiliated with the Gift of the Givers Foundation.*
