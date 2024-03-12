using HomeAppApi.Models;

namespace HomeAppApi.Interfaces
{
    public interface IHouseRepository
    {
        ICollection<House> GetHouses();
        House GetHouse(int houseId);
        ICollection<House> GetHousesByCity(int cityId);
        ICollection<House> GetHousesByState(int stateId);
        bool IsAvailable(int houseId);
        int CreateHouse(House house);
    }
}
