using HomeAppApi.Data;
using HomeAppApi.Models;
using HomeAppApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HomeAppApi.Repository 
{
    public class HouseRepository : IHouseRepository
    {
        private readonly DataContext _context;

        public HouseRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<House> GetHouses()
        {
            return _context.Houses.OrderBy(h => h.Id).Include(h => h.City).Include(h => h.State).ToList();
        }

        public House GetHouse(int houseId) 
        {
            return _context.Houses.Where(h => h.Id == houseId).Include(h => h.City).Include(h => h.State).FirstOrDefault();
        }

        public ICollection<House> GetHousesByCity(int cityId)
        {
            return _context.Houses.Where(h => h.CityId == cityId).Include(h => h.City).Include(h => h.State).ToList();
        }

        public ICollection<House> GetHousesByState(int stateId)
        {
            return _context.Houses.Where(h => h.StateId == stateId).Include(h => h.City).Include(h => h.State).ToList();
        }



        public bool IsAvailable(int houseId)
        {
            return _context.Houses.Where(h => h.Id == houseId).Any();
        }

        public int CreateHouse(House house)
        {
            _context.Add(house);
            _context.SaveChanges();
            return house.Id;
        }

    }
}
