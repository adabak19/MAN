using MAN.Shared.Models;
using MAN.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using MAN.Client.Services;
using MAN.Client.Auth;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using Microsoft.AspNetCore.Components.Authorization;
using ServiceStack.Blazor;

// using Microsoft.AspNetCore.Components.Authorization;

// var builder = WebAssemblyHostBuilder.CreateDefault(args);
// builder.RootComponents.Add<App>("#app");
// builder.RootComponents.Add<HeadOutlet>("head::after");
// builder.Services.AddRazorComponents()
//     .AddInteractiveServerComponents();

// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7216/") });
// builder.Services.AddAuthentication().AddCookie(options => {
//     options.LoginPath = "/login";
// });
// // builder.Services.AddOidcAuthentication(options =>
// // {
// //     builder.Configuration.Bind("OidcProvider", options.ProviderOptions);
// // });
// builder.Services.AddScoped<IBookService, BookService>();
// builder.Services.AddScoped<IAuthorService, AuthorService>();
// builder.Services.AddScoped<IGenreService, GenreService>();
// builder.Services.AddScoped<IBindingTypeService, BindingTypeService>();
// builder.Services.AddScoped<IBookGenreService, BookGenreService>();
// builder.Services.AddScoped<IBookReadService, BookReadService>();
// builder.Services.AddScoped<ICoAuthorService, CoAuthorService>();
// builder.Services.AddScoped<IProfileService, ProfileService>();
// builder.Services.AddScoped<IPublisherService, PublisherService>();
// builder.Services.AddScoped<IAuthServiceWEB, JwtAuthService>();
// builder.Services.AddAuthorizationCore(); // For Blazor Authentication
// builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
// AuthorizationPolicies.AddPolicies(builder.Services);
// // await builder.Build().RunAsync();

// var app = builder.Build();

// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error", createScopeForErrors: true);
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.UseStaticFiles();
// app.UseAntiforgery();

// app.MapRazorComponents<App>()
//     .AddInteractiveServerRenderMode();

// app.Run();
// using Microsoft.AspNetCore.Components.Web;
// using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
// using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
// using MAN.Client.Services;
// using MAN.Client.Auth;
// using MAN.Shared.Interfaces;
// using MAN.Shared.Models;
// using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register root components
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient with base address
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7216/") });

// Configure authentication
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<CustomAuthProvider>();
builder.Services.AddScoped<IAuthServiceWEB, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthProvider>());


// Optional: Configure OIDC Authentication if you have the necessary configuration
// builder.Services.AddOidcAuthentication(options =>
// {
//     builder.Configuration.Bind("OidcProvider", options.ProviderOptions);
// });

// Register application services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IBindingTypeService, BindingTypeService>();
builder.Services.AddScoped<IBookGenreService, BookGenreService>();
builder.Services.AddScoped<IBookReadService, BookReadService>();
builder.Services.AddScoped<ICoAuthorService, CoAuthorService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddLocalStorage();

// Register authorization policies (if you are using custom policies)

// Build and run the application
await builder.Build().RunAsync();