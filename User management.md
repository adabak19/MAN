# User Management

## User Types

Our system supports two distinct user roles:

1. **Admin**
   - Admins are library staff with elevated privileges.
   - Responsibilities include managing books, users, and administrative tasks.

2. **User**
   - Users are library members.
   - They can borrow books, view their borrowed books, and manage their profiles.

---

## Log-In Functionality

The log-in functionality is implemented using **JWT (JSON Web Token)** authentication to ensure secure access to resources.

### Overview

1. **Login Page**  
   Users input their credentials (profile name and password) on the login page.

2. **Authentication Service**  
   Credentials are validated against the backend. If valid, a JWT is generated and returned.

3. **Token Storage**  
   The JWT is stored in the browser's local storage and used for subsequent authenticated API requests.

### Key Code Snippets

#### Login Page
The `loginModel` captures the profile name and password. Upon submission, the `AuthService.LoginAsync` method authenticates the user.
```razor
@page "/login"
@using MAN.Shared.DTO

<EditForm Model="loginModel" OnValidSubmit="HandleLogin">
    <InputText id="profileName" class="form-control" @bind-Value="loginModel.ProfileName" />
    <InputText id="password" type="password" class="form-control" @bind-Value="loginModel.Password" />
    <button type="submit">Login</button>
</EditForm>

@code {
    private ProfileLoginDto loginModel = new();
    private async Task HandleLogin() => await AuthService.LoginAsync(loginModel.ProfileName, loginModel.Password);
}
```
#### JWT Authentication Service
Handles the authentication logic, token storage, and parsing claims from the JWT.
```csharp
public class JwtAuthService(HttpClient client, IJSRuntime jsRuntime) : IAuthServiceWEB {
    public async Task LoginAsync(string username, string password) {
        var profileLoginDto = new ProfileLoginDto { ProfileName = username, Password = password };
        HttpResponseMessage response = await client.PostAsync("/auth/login", 
            new StringContent(JsonSerializer.Serialize(profileLoginDto), Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode) {
            Jwt = await response.Content.ReadAsStringAsync();
            await CacheTokenAsync();
        }
    }

    private async Task CacheTokenAsync() => await jsRuntime.InvokeVoidAsync("localStorage.setItem", "jwt", Jwt);
    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt) {
        string payload = jwt.Split('.')[1];
        return JsonSerializer.Deserialize<Dictionary<string, object>>(Convert.FromBase64String(payload))
            .Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }
}
```
## Access Control and Authorization Policies
### Resource Access
Different resources are accessible based on the user type:

#### Admins:
Access to administrative resources such as the dashboard, user management, and book management.
#### Users:
Access to personal resources like their borrowed books and profile management.

### Authorization Policies
Authorization is implemented using role-based claims, enforced through ASP.NET Core's authorization system.
#### Code Snippet: Authorization Policies
```csharp
public static class AuthorizationPolicies {
    public static void AddPolicies(IServiceCollection services) {
        services.AddAuthorizationCore(options => {
            options.AddPolicy("MustBeAdmin", policy => 
                policy.RequireAuthenticatedUser().RequireClaim("Role", "Admin"));
            options.AddPolicy("MustBeMember", policy => 
                policy.RequireAuthenticatedUser().RequireClaim("Role", "Member"));
        });
    }
}
```
#### Example: Restricting Admin Dashboard Access
```csharp
@page "/dashboard"
<AuthorizeView Policy="MustBeAdmin">
    <NotAuthorized>
        <p>You must be an admin to access this page.</p>
    </NotAuthorized>
    <Authorized>
        <h3>Admin Dashboard</h3>
        <div class="dashboard-container">
            <div @onclick="NavigateToActiveLoans">Active Loans</div>
            <div @onclick="NavigateToBooks">Books</div>
            <div @onclick="NavigateToUsers">Users</div>
        </div>
    </Authorized>
</AuthorizeView>
```
## Key Features of the Implementation
- Role-Based Access Control: JWT tokens store the user's role as a claim. Authorization policies ensure then role-specific access to resources.
- Decentralized Authorization: Role verification is performed in the front-end components using AuthorizeView or backend policies.
- Secure Token Storage: JWTs are securely stored in the browser's local storage to persist user sessions.
- Dynamic UI Changes: Navigation menus and resource access dynamically adapt based on the authenticated user's role. For example, when there is no user logged in, they will be able to see home, login and register only. Then admins also have their own dashboard.

## Conclusion
By implementing JWT-based authentication and role-based authorization, the system effectively differentiates between user and admin functionalities. This ensures secure and controlled access to resources while providing a seamless experience for both user types.


