using HomeAppApi.Data;
using HomeAppApi.Interfaces;
using HomeAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeAppApi.Repository
{
    public class StateRepository : IStateRepository
    {
        private readonly DataContext _context;

        public StateRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<State> GetStates()
        {
            return _context.States.OrderBy(c => c.Name).ToList();
        }

        public State GetState(int stateId)
        {
            return _context.States.Where(c => c.StateId == stateId).FirstOrDefault();
        }

        public bool IsAvailable(int stateId)
        {
            return _context.States.Where(c => c.StateId == stateId).Any();
        }

        public int CreateState(State state)
        {
            _context.Add(state);
            _context.SaveChanges();
            return state.StateId;
        }

    }
}
