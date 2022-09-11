using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();

// Cors is not needed for this scenario, but is added to show how to configure it.
// builder.Services.AddCors();

builder.Services.AddBff(o => o.ManagementBasePath = "/account")
    .AddServerSideSessions();

builder.Services.AddAuthentication(o => 
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = "oidc";
    o.DefaultSignOutScheme = "oidc";
})
    .AddCookie(o => 
    {
        o.Cookie.Name = "__Host-spa";
        o.Cookie.SameSite = SameSiteMode.Strict;

        o.Events.OnRedirectToLogin = (context) =>
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:4001";

        // confidential client using code flow + PKCE + query response mode
        options.ClientId = "SpaDemo";
        options.ClientSecret = "secret"; // should come from configuration, just for the demo purposes!
        options.ResponseType = "code";
        options.ResponseMode = "query";
        options.UsePkce = true;

        options.MapInboundClaims = false;
        options.GetClaimsFromUserInfoEndpoint = true;

        // save access and refresh token to enable automatic lifetime management
        options.SaveTokens = true;

        // request scopes
        options.Scope.Add("Spa.Api.basicAccess");
        options.Scope.Add("roles");

        // request refresh token
        options.Scope.Add("offline_access");
    });
builder.Services.AddAuthorization(options => 
    {
        options.AddPolicy("admin", policy => policy.RequireClaim("role", "admin"));
    });

builder.Services.AddDbContext<HouseDbContext>(options => 
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IHouseRepository, HouseRepository>();
builder.Services.AddScoped<IBidRepository, BidRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseAuthentication();

// Cors is not needed for this scenario, but it is a good practice to have it
// app.UseCors(p => p.WithOrigins("http://localhost:3000")
//     .AllowAnyHeader()
//     .AllowAnyMethod());

// app.UseHttpsRedirection();

app.MapHouseEndpoints();
app.MapBidEndpoints();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(e => e.MapBffManagementEndpoints());
app.MapFallbackToFile("index.html");

app.Run();
