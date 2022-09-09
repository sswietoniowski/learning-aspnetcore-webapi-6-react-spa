using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<HouseDbContext>(options => 
{
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});
builder.Services.AddScoped<IHouseRepository, HouseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseHttpsRedirection();

app.MapGet("/houses", (IHouseRepository houseRepository) =>
{
    return houseRepository.GetHouses();
}).WithName("GetHouses")
    .Produces<List<HouseDto>>(StatusCodes.Status200OK);

app.MapGet("/houses/{houseId:int}", async (int houseId, IHouseRepository houseRepository) =>
{
    var house = await houseRepository.GetHouse(houseId);
    if (house == null)
    {
        return Results.Problem($"House with ID {houseId} not found", 
            statusCode: 404);
    }
    return Results.Ok(house);
}).WithName("GetHouse")
    .ProducesProblem(StatusCodes.Status404NotFound)
    .Produces<HouseDetailsDto>(StatusCodes.Status200OK);

app.MapPost("/houses", async ([FromBody] HouseDetailsDto houseDto, IHouseRepository houseRepository) =>
{
    var createdHouse = await houseRepository.AddHouse(houseDto);
    return Results.Created($"/houses/{createdHouse.Id}", createdHouse);
}).WithName("AddHouse")
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .Produces<HouseDetailsDto>(StatusCodes.Status201Created);

app.MapPut("/houses/{houseId:int}", async (int houseId, [FromBody] HouseDetailsDto houseDto, IHouseRepository houseRepository) =>
{
    var house = await houseRepository.GetHouse(houseId);
    if (house == null)
    {
        return Results.Problem($"House with ID {houseId} not found", 
            statusCode: 404);
    }
    var updatedHouse = await houseRepository.UpdateHouse(houseDto with { Id = houseId });
    return Results.Ok(updatedHouse);
}).WithName("UpdateHouse")
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .Produces<HouseDetailsDto>(StatusCodes.Status200OK);    

app.MapDelete("/houses/{houseId:int}", async (int houseId, IHouseRepository houseRepository) =>
{
    var house = await houseRepository.GetHouse(houseId);
    if (house == null)
    {
        return Results.Problem($"House with ID {houseId} not found", 
            statusCode: 404);
    }
    await houseRepository.DeleteHouse(houseId);
    return Results.NoContent();
}).WithName("DeleteHouse")
    .ProducesProblem(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status204NoContent);

app.Run();
