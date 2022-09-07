public interface IHouseRepository
{
    public Task<List<HouseDto>> GetHouses();
}