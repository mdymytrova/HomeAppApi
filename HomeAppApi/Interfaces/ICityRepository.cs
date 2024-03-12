using HomeAppApi.Models;

namespace HomeAppApi.Interfaces
{
    public interface ICityRepository
    {
        ICollection<City> GetCities(CityFilter cityFilter);
        City GetCity(int cityId);
        ICollection<City> GetCitiesByState(int stateId);
        bool IsAvailable(int cityId);
        int CreateCity(City city);
    }
}
