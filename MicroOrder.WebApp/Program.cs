using MicroOrder.ProductService.Client;
using MicroOrder.BasketService.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using MicroOrder.WebApp.Infrastructure.Authentication;
using MicroOrder.WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<LogOutService, LogOutService>();
builder.Services.AddProductService(builder.Configuration);
builder.Services.AddBasketService(builder.Configuration);

// Auth
builder.Services.AddSingleton<ITicketStore, MemoryTicketStore>();
builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.SessionStore = new MemoryTicketStore();
        options.Cookie.Name = "mycookie";
    })
    .AddOpenIdConnect(options =>
    {
        options.Authority = "https://localhost:5001";

        options.ClientId = "microorder.webapp";
        options.ClientSecret = "B20DADFE-DA71-4BE1-97FA-8CBDCDF201C3";

        options.ResponseType = "code";
        options.UsePkce = true;

        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;

        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("orderserviceapi.fullaccess");
        options.Scope.Add("productserviceapi.fullaccess");
        options.Scope.Add("basketserviceapi.fullaccess");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
