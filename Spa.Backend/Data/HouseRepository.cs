using Microsoft.EntityFrameworkCore;

public class HouseRepository : IHouseRepository
{
    private readonly HouseDbContext dbContext;

    public HouseRepository(HouseDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<HouseDto>> GetHouses()
    {
        return await dbContext.Houses
            .Select(h => new HouseDto(h.Id, h.Address, h.Country, h.Price))
            .ToListAsync();
    }
}