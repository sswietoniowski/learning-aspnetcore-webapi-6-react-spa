using Microsoft.EntityFrameworkCore;

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext dbContext;

    public HouseRepository(HouseDbContext dbContext)
    {
        this.dbContext = dbContext;
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
}