using Microsoft.AspNetCore.Mvc;
using HomeAppApi.Interfaces;
using HomeAppApi.Models;
using HomeAppApi.Dto;
using AutoMapper;
using HomeAppApi.Repository;
using System.Diagnostics;

namespace HomeAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : Controller
    {
        public readonly IHouseRepository _houseRepository;
        public readonly ICityRepository _cityRepository;
        public readonly IStateRepository _stateRepository;
        public readonly IOwnerRepository _ownerRepository;
        public readonly IMapper _mapper;

        public HouseController(IHouseRepository houseRepository, IMapper mapper, ICityRepository cityRepository, IStateRepository stateRepository, IOwnerRepository ownerRepository)
        {
            _houseRepository = houseRepository;
            _cityRepository = cityRepository;
            _stateRepository = stateRepository;
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HouseDto>))]
        public IActionResult GetHouses()
        {
            var houses = _mapper.Map<List<HouseDto>>(_houseRepository.GetHouses());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(houses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(HouseDto))]
        [ProducesResponseType(400)]

        public IActionResult GetHouse(int id)
        {
            if (!_houseRepository.IsAvailable(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var house = _mapper.Map<HouseDto>(_houseRepository.GetHouse(id));
            return Ok(house);
        }

        [HttpGet("city/{cityId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HouseDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetHousesByCity(int cityId)
        {
            if (!_cityRepository.IsAvailable(cityId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var houses = _mapper.Map<List<HouseDto>>(_houseRepository.GetHousesByCity(cityId));
            return Ok(houses);
        }

        [HttpGet("state/{stateId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<HouseDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetHousesByState(int stateId)
        {
            if (!_stateRepository.IsAvailable(stateId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var houses = _mapper.Map<List<HouseDto>>(_houseRepository.GetHousesByState(stateId));
            return Ok(houses);
        }


        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateHouse([FromBody] HouseDto houseCreate)
        {
            if (houseCreate == null)
            {
                return BadRequest(ModelState);
            }

            var house = _houseRepository.GetHouses()
                .Where(c => c.Name.Trim().ToUpper() == houseCreate.Name.Trim().ToUpper())
                .FirstOrDefault();


            if (house != null)
            {
                ModelState.AddModelError("exist", "House already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var houseMap = _mapper.Map<House>(houseCreate);
            houseMap.State = _stateRepository.GetState(houseCreate.StateId);

            var cityId = houseCreate.CityId;
            if (!Convert.ToBoolean(cityId))
            {
                cityId = _cityRepository.CreateCity(new City()
                {
                    Name = houseCreate.CityName,
                    StateId = houseCreate.StateId
                });
            }

            houseMap.City = _cityRepository.GetCity(cityId);
            houseMap.Owner = _ownerRepository.GetOwner(houseCreate.OwnerId);

            try
            {
                _houseRepository.CreateHouse(houseMap);
                return Ok("Successfully created");
            } catch (Exception e)
            {
                ModelState.AddModelError("create", "Cannot create house");
                return StatusCode(500, ModelState);
            }

            
        }
    }
}
