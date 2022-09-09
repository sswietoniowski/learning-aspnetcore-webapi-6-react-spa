using Microsoft.EntityFrameworkCore;

public interface IBidRepository
{
    Task<List<BidDto>> GetBid(int houseId);
    Task<BidDto> AddBid(BidDto bid);
}

public class BidRepository: IBidRepository
{
    private readonly HouseDbContext context;

    public BidRepository(HouseDbContext context)
    {
        this.context = context;
    }

    public async Task<List<BidDto>> GetBid(int houseId)
    {
        return await context.Bids.Where(b => b.HouseId == houseId)
            .Select(b => new BidDto(b.Id, b.HouseId, b.Bidder, b.Amount))
            .ToListAsync();
    }

    public async Task<BidDto> AddBid(BidDto dto)
    {
        var entity = new BidEntity();
        entity.HouseId = dto.HouseId;
        entity.Bidder = dto.Bidder;
        entity.Amount = dto.Amount;
        context.Bids.Add(entity);
        await context.SaveChangesAsync();
        return new BidDto(entity.Id, entity.HouseId, 
            entity.Bidder, entity.Amount);
    }
}