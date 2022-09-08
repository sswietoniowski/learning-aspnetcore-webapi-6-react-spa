public interface IHouseRepository
{
    public Task<List<HouseDto>> GetHouses();
    public Task<HouseDetailsDto?> GetHouse(int id);
}