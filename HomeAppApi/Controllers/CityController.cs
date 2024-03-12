using AutoMapper;
using HomeAppApi.Interfaces;
using HomeAppApi.Models;
using HomeAppApi.Dto;
using Microsoft.AspNetCore.Mvc;
using HomeAppApi.Repository;

namespace HomeAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : Controller
    {
        public readonly ICityRepository _cityRepository;
        public readonly IStateRepository _stateRepository;
        public readonly IMapper _mapper;

        public CityController(ICityRepository cityRepository, IStateRepository stateRepository, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _mapper = mapper;
        }

        [HttpPost("Cities")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CityDto>))]
        public IActionResult GetCities([FromBody] CityFilter cityFilter)
        {
            var srcCities = _cityRepository.GetCities(cityFilter);
            var cities = _mapper.Map<List<CityDto>>(srcCities);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(cities);
        }

        [HttpGet("{cityId}")]
        [ProducesResponseType(200, Type = typeof(CityDto))]
        [ProducesResponseType(400)]

        public IActionResult GetCity(int cityId)
        {
            if (!_cityRepository.IsAvailable(cityId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = _mapper.Map<CityDto>(_cityRepository.GetCity(cityId));
            return Ok(city);
        }

        [HttpGet("{stateId}/cities")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CityDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetCitiesByState(int stateId)
        {
            if (!_stateRepository.IsAvailable(stateId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cities = _mapper.Map<List<CityDto>>(_cityRepository.GetCitiesByState(stateId));
            return Ok(cities);
        }



        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCity([FromBody] CityDto cityCreate)
        {
            if (cityCreate == null)
            {
                return BadRequest(ModelState);
            }

            var city = _cityRepository.GetCities(new CityFilter()
            {
                includeWithoutHouses = false
            })
                .Where(c => c.Name.Trim().ToUpper() == cityCreate.Name.Trim().ToUpper() && c.StateId == cityCreate.StateId)
                .FirstOrDefault();


            if (city != null)
            {
                ModelState.AddModelError("", "City already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityMap = _mapper.Map<City>(cityCreate);
            cityMap.State = _stateRepository.GetState(cityCreate.StateId);

            if (!Convert.ToBoolean(_cityRepository.CreateCity(cityMap)))
            {
                ModelState.AddModelError("", "Cannot create city");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}
