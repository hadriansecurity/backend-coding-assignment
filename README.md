# Backend Coding Assignment
Contains the coding assignment for the backend team onboarding process.

This repository contains a foundational project structure, including API, a database model and a test project.
The task will be to add a new API endpoint for Slack integration.

# Prerequisites

## Required

- Dotnet SDK: https://dotnet.microsoft.com/en-us/download
- Docker: https://www.docker.com/get-started/

## Optional
- An IDE. We use one of the following:
  - VS Code: https://code.visualstudio.com/ (free)
  - JetBrains Rider: https://www.jetbrains.com/rider/ (30-day trial)

# Building

Open a terminal in the root directory of this repository and execute the following commands:

- `cd src`
- `dotnet build`

# Testing

Open a terminal in the root directory of this repository and execute the following commands:

- `cd src`
- `dotnet test`

# Running

You can run the application in a few ways.
Whichever way you run it, once its running, you can access the swagger docs by navigating to:

`http://localhost:5176/swagger/index.html`
The port number can vary based on your system settings, see the applications console output for the actual port used.

## Using the terminal

Open a terminal in the root directory of this repository and execute the following commands:

- `docker compose up` (this will launch a local database to use for testing purposes)
- `cd src/Hadrian.CodingAssignment.Api`
- `dotnet run`

## Using VsCode

Press "F5". there is only 1 configuration, which will build and launch the application.
A browser that opens the swagger documentation page will automatically open.
