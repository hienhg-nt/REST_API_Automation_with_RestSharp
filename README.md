# NunitDemo

NunitDemo is a sample NUnit test project for REST API automation using RestSharp.
It contains test suites, services, models, test data, and helpers to demonstrate API testing patterns in .NET.

**Contents**
- **Project:** NunitDemo (NUnit + RestSharp based API tests)
- **Language:** C# (.NET 9)

**Prerequisites**
- .NET 9 SDK installed: https://dotnet.microsoft.com/download
- A code editor (Visual Studio, VS Code) or CLI

**Quick start**
- Restore dependencies and build the solution:

```powershell
dotnet restore NunitDemo.sln
dotnet build NunitDemo.sln
```

- Run all tests:

```powershell
dotnet test NunitDemo.sln
```

**Repository structure (important paths)**
- src/Configuration/appsetting.json — environment/config values
- src/Core/APIClient.cs — HTTP client wrapper
- src/Service/ — service layer (BaseService.cs, BookService.cs, UserService.cs)
- src/Models/ — request/response models
- src/Data/ — test data and JSON schemas
- src/Helpers/ — helpers for config and data reading
- src/Tests/ — NUnit test classes (BookTest, UserTest, etc.)

**Configuration**
- Edit src/Configuration/appsetting.json to set base API URL and environment-specific settings.

**Adding tests**
- Add test classes under src/Tests/ and use test case sources from src/TestCaseSources/.
- Use the Service layer to keep tests focused on assertions and flows.

**Notes**
- Tests use NUnit attributes and assertions. Test data is stored in src/Data/ as JSON files.
- REST calls are performed via APIClient/RestSharp wrappers in src/Core/.

**Contributing**
- Open issues or submit PRs with clear descriptions and test coverage where appropriate.

**License**
- Add an appropriate license file if you intend to open-source this project.