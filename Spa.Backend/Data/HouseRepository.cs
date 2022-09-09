using Microsoft.EntityFrameworkCore;

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext dbContext;

    public HouseRepository(HouseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<HouseDetailsDto> AddHouse(HouseDetailsDto houseDto)
    {
        var house = new HouseEntity
        {
            Address = houseDto.Address,
            Country = houseDto.Country,
            Description = houseDto.Description,
            Price = houseDto.Price,
            Photo = houseDto.Photo
        };

        await dbContext.Houses.AddAsync(house);    
        await dbContext.SaveChangesAsync();

        return houseDto with { Id = house?.Id ?? 0 };
    }

    public async Task DeleteHouse(int houseId)
    {
        var house = dbContext.Houses.Find(houseId);
        if (house != null)
        {
            dbContext.Houses.Remove(house);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<HouseDetailsDto?> GetHouse(int id)
    {
        var house = await dbContext.Houses.FindAsync(id);
        if (house == null)
        {
            return null;
        }
        return new HouseDetailsDto(
            house.Id, house.Address, house.Country, 
            house.Description, house.Price, house.Photo);
    }

    public async Task<List<HouseDto>> GetHouses()
    {
        return await dbContext.Houses
            .Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price))
            .ToListAsync();
    }

    public async Task<HouseDetailsDto> UpdateHouse(HouseDetailsDto houseDto)
    {
        var house = new HouseEntity
        {
            Id = houseDto.Id,
            Address = houseDto.Address,
            Country = houseDto.Country,
            Description = houseDto.Description,
            Price = houseDto.Price,
            Photo = houseDto.Photo
        };

        dbContext.Houses.Update(house);
        await dbContext.SaveChangesAsync();

        return houseDto;
    }
}