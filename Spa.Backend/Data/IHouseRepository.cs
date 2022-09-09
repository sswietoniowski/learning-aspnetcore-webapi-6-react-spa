public interface IHouseRepository
{
    public Task<List<HouseDto>> GetHouses();
    public Task<HouseDetailsDto?> GetHouse(int id);
    Task<HouseDetailsDto> AddHouse(HouseDetailsDto houseDto);
    Task<HouseDetailsDto> UpdateHouse(HouseDetailsDto houseDto);
    Task DeleteHouse(int houseId);
}