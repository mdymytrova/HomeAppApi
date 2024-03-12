using HomeAppApi.Models;

namespace HomeAppApi.Interfaces
{
    public interface IStateRepository
    {
        ICollection<State> GetStates();
        State GetState(int id);
        bool IsAvailable(int stateId);
        int CreateState(State state);
    }
}
