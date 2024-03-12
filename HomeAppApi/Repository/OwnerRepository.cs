using HomeAppApi.Data;
using HomeAppApi.Interfaces;
using HomeAppApi.Models;

namespace HomeAppApi.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;

        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(c => c.FirstName).ToList();
        }

        public Owner GetOwner(int ownerId)
        {
            return _context.Owners.Where(c => c.OwnerId == ownerId).FirstOrDefault();
        }

        public bool IsAvailable(int ownerId)
        {
            return _context.Owners.Where(c => c.OwnerId == ownerId).Any();
        }

        public int CreateOwner(Owner owner)
        {
            _context.Add(owner);
            _context.SaveChanges();
            return owner.OwnerId;
        }
    }
}
