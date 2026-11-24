# CakeCraft
Full-stack starter created from the supplied specification: React UI, ASP.NET Core 9 Web API, EF Core SQL Server, Identity/JWT roles, Baker CRUD, Customer browsing/search, Key Vault hook, Swagger, and GitHub Actions.

## Run backend
1. Update `dotnetapp/appsettings.json` with a safe SQL connection string and a 32+ character JWT secret.
2. `cd dotnetapp`
3. `dotnet tool restore` (after creating a local manifest if needed)
4. `dotnet ef migrations add InitialSetup`
5. `dotnet ef database update`
6. `dotnet run --urls http://localhost:8080`

## Run frontend
1. `cd reactapp`
2. `npm install`
3. `npm start` (defaults to port 3000)

Set `REACT_APP_API_BASE_URL=http://localhost:8080` when the API is elsewhere.

## Azure CI/CD
The GitHub Actions workflows in `.github/workflows` validate changes to each app on pull requests targeting `main`. Pushes to `main` build and deploy the changed app to its Azure App Service; `workflow_dispatch` can also build either workflow manually, and deploys only when run on `main`.

Create separate Linux Azure App Services for the API and frontend. Configure these GitHub repository variables:

| Variable | Value |
| --- | --- |
| `BACKEND_APP_NAME` | Azure App Service name for the ASP.NET Core API |
| `FRONTEND_APP_NAME` | Azure App Service name for the React site |
| `API_BASE_URL` | Public HTTPS base URL for the deployed API, without a trailing slash |

Add these GitHub repository secrets using the publish profiles downloaded from each App Service's **Get publish profile** action:

| Secret | Value |
| --- | --- |
| `BACKEND_PUBLISH_PROFILE` | Publish profile for the API App Service |
| `FRONTEND_PUBLISH_PROFILE` | Publish profile for the frontend App Service |

Use the .NET 9 runtime stack on the API App Service and Node.js 20 on the frontend App Service. Configure the frontend App Service startup command as `pm2 serve /home/site/wwwroot --no-daemon --spa` so React client-side routes resolve correctly.

Set production SQL connection and JWT settings in the API App Service configuration or Azure Key Vault; never put production credentials in GitHub variables or source control. The API uses the `ConnectionStrings__DefaultConnection` and `JWT__Secret` environment variable names. Set `KeyVaultConfiguration__URL` if using Key Vault and grant the App Service managed identity permission to read its secrets. Database migrations must be applied to the production database as a separately controlled deployment operation.

## Important
Replace all sample credentials before deployment. The implementation uses image URLs in the UI. A production extension can upload files to Blob Storage and persist the returned URL in `CakeImage`.
