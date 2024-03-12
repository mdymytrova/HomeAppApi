using HomeAppApi.Models;

namespace HomeAppApi.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        bool IsAvailable(int ownerId);
        int CreateOwner(Owner owner);
    }
}
