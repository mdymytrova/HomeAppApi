using Microsoft.AspNetCore.Mvc;
using HomeAppApi.Interfaces;
using HomeAppApi.Models;
using HomeAppApi.Dto;
using AutoMapper;
using HomeAppApi.Repository;

namespace HomeAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : Controller
    {
        public readonly IStateRepository _stateRepository;
        public readonly IMapper _mapper;

        public StateController(IStateRepository stateRepository, IMapper mapper)
        {
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StateDto>))]
        public IActionResult GetStates()
        {
            var states = _mapper.Map<List<StateDto>>(_stateRepository.GetStates());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(states);
        }

        [HttpGet("{stateId}")]
        [ProducesResponseType(200, Type = typeof(StateDto))]
        [ProducesResponseType(400)]

        public IActionResult GetState(int stateId)
        {
            if (!_stateRepository.IsAvailable(stateId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var state = _mapper.Map<StateDto>(_stateRepository.GetState(stateId));
            return Ok(state);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateState([FromBody] StateDto stateCreate)
        {
            if (stateCreate == null)
            {
                return BadRequest(ModelState);
            }

            var state = _stateRepository.GetStates()
                .Where(c => c.Name.Trim().ToUpper() == stateCreate.Name.Trim().ToUpper())
                .FirstOrDefault();


            if (state != null)
            {
                ModelState.AddModelError("", "State already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stateMap = _mapper.Map<State>(stateCreate);

            if (!Convert.ToBoolean(_stateRepository.CreateState(stateMap)))
            {
                ModelState.AddModelError("", "Cannot create state");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}

