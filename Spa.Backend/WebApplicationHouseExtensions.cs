using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniValidation;

public static class WebApplicationHouseExtensions
{
    public static void MapHouseEndpoints(this WebApplication app)
    {
        app.MapGet("/houses", [Authorize("admin")](IHouseRepository houseRepository) =>
        {
            return houseRepository.GetHouses();
        }).WithName("GetHouses")
            .Produces<List<HouseDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);

        app.MapGet("/houses/{houseId:int}", [Authorize]async (int houseId, IHouseRepository houseRepository) =>
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

        app.MapPost("/houses", [Authorize]async ([FromBody] HouseDetailsDto houseDto, IHouseRepository houseRepository) =>
        {
            if (!MiniValidator.TryValidate(houseDto, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            var createdHouse = await houseRepository.AddHouse(houseDto);
            return Results.Created($"/houses/{createdHouse.Id}", createdHouse);
        }).WithName("AddHouse")
            .ProducesValidationProblem()
            .Produces<HouseDetailsDto>(StatusCodes.Status201Created);

        app.MapPut("/houses/{houseId:int}", [Authorize]async (int houseId, [FromBody] HouseDetailsDto houseDto, IHouseRepository houseRepository) =>
        {
            if (!MiniValidator.TryValidate(houseDto, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            var house = await houseRepository.GetHouse(houseId);
            if (house == null)
            {
                return Results.Problem($"House with ID {houseId} not found", 
                    statusCode: 404);
            }
            var updatedHouse = await houseRepository.UpdateHouse(houseDto with { Id = houseId });
            return Results.Ok(updatedHouse);
        }).WithName("UpdateHouse")
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .Produces<HouseDetailsDto>(StatusCodes.Status200OK);    

        app.MapDelete("/houses/{houseId:int}", [Authorize]async (int houseId, IHouseRepository houseRepository) =>
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

    }
}