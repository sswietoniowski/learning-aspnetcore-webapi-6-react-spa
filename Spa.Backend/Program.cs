using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", o => 
    {
        o.Authority = "https://localhost:4001";
        o.Audience = "Spa.Api";
        o.MapInboundClaims = false;
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

app.UseCors(p => p.WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapHouseEndpoints();
app.MapBidEndpoints();

app.UseAuthorization();

app.Run();
