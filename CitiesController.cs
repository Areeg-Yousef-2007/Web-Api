using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.DTOs.City;
using WeatherApp.Repos.CityRepo;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepo _cityRepo;

        public CitiesController(ICityRepo cityRepo)
        {
            _cityRepo = cityRepo;
        }


        [HttpGet]
        public ActionResult<List<CityResponseDto>> GetAllCities()
        {
            var cities = _cityRepo.GetAllCities();
        
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityResponseDto> GetCity(int id)
        {
            var city = _cityRepo.GetCityById(id);
            if (city == null)
            {
                return NotFound($"City with ID {id} not found.");
            }
            _cityRepo.GetCityById(id);
            return Ok(city);
        }

        [HttpPost]
        public ActionResult AddCity(CityRequestDto cityRequestDto)
        {
            if (cityRequestDto == null)
            {
                return BadRequest("Invalid data.");
            }

            _cityRepo.AddCity(cityRequestDto);

            return Created();
        }
    }
}







