# Ticketing API

## Configuration

### Connection String Setup

This project uses user secrets for local development to keep sensitive information like connection strings out of the repository.

#### For Local Development:

1. Initialize user secrets (if not already done):

   ```bash
   dotnet user-secrets init
   ```

2. Set your connection string:

   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your-connection-string-here"
   ```

3. For production, use environment variables:
   ```bash
   setx ConnectionStrings__DefaultConnection "your-production-connection-string"
   ```

#### Available Configuration:

- **ConnectionStrings:DefaultConnection**: Database connection string
- **ApiSettings:MaxPageSize**: Maximum number of items per page (default: 100)
- **ApiSettings:DefaultPageSize**: Default number of items per page (default: 20)
- **Cors:AllowedOrigins**: Allowed origins for CORS policy

### Running the Application

1. Build the project:

   ```bash
   dotnet build
   ```

2. Run the application:

   ```bash
   dotnet run
   ```

3. The API will be available at `https://localhost:7001` (or the configured port)

### Testing

Run the tests:

```bash
dotnet test
```

## Security Notes

- Never commit sensitive information like connection strings to the repository
- Use user secrets for local development
- Use environment variables or Azure Key Vault for production deployments
- The `appsettings.json` file contains only non-sensitive configuration
