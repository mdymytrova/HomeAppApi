using HomeAppApi.Data;
using HomeAppApi.Interfaces;
using HomeAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeAppApi.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext _context;

        public CityRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<City> GetCities(CityFilter cityFilter)
        {
            var cities = _context.Cities.Where(c => EF.Functions.Like(c.Name.Trim().ToLower(), "%" + cityFilter.search + "%")).OrderBy(c => c.Name).Include(c => c.State).Include(c => c.Houses).ToList();
            if (cityFilter.includeWithoutHouses)
            {
                return cities;
            } else
            {
                var citiesWithHouses = cities.Where(c => c.Houses.Any()).ToList();
                return citiesWithHouses;
            }
        }

        public City GetCity(int cityId)
        {
            return _context.Cities
                .Where(c => c.CityId == cityId).Include(c => c.State).Include(c => c.Houses).FirstOrDefault();
        }

        public ICollection<City> GetCitiesByState(int stateId)
        {
            return _context.Cities.Where(c => c.StateId == stateId).Include(c => c.State).ToList();
        }

        public bool IsAvailable(int cityId)
        {
            return _context.Cities.Where(c => c.CityId == cityId).Any();
        }

        public int CreateCity(City city)
        {
            _context.Add(city);
            _context.SaveChanges();
            return city.CityId;
        }
    }
}
