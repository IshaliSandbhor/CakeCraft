# CakeCraft
Full-stack starter created from the supplied specification: React UI, ASP.NET Core 8 Web API, EF Core SQL Server, Identity/JWT roles, Baker CRUD, Customer browsing/search, Key Vault hook, Swagger, and GitHub Actions.

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

## Azure notes
- Set `KeyVaultConfiguration__URL` for Key Vault and use workload identity or a service principal with secret-read access.
- Configure SQL connection/JWT values through Key Vault or App Service settings, not source control.
- Configure GitHub repository variables `BACKEND_APP_NAME`, `FRONTEND_APP_NAME`, `API_BASE_URL`, and publish-profile secrets used by the workflows.
- Frontend App Service startup command: `pm2 serve /home/site/wwwroot --no-daemon --spa`.

## Important
Replace all sample credentials before deployment. The implementation uses image URLs in the UI. A production extension can upload files to Blob Storage and persist the returned URL in `CakeImage`.
