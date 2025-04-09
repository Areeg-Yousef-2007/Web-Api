using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.DTOs.Forecast;
using WeatherApp.Repos.ForecastRepo;
using WeatherApp.Repos.WeatherRepo;

namespace WeatherApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastsController : ControllerBase
    {
        private readonly IForecastRepo _forecastRepo;
        public ForecastsController(IForecastRepo forecastRepo)
        {
            _forecastRepo= forecastRepo;

        }
        


        [HttpGet("{cityId}")]
        public ActionResult<List<ForecastResponseDto>> GetForecastsByCityId(int cityId)
        {
            try
            {
                var forecasts = _forecastRepo.GetForecastsByCityId(cityId);
                if (forecasts == null || forecasts.Count == 0)
                {
                    return NotFound();
                }

                return Ok(forecasts); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }

        [HttpPost]
        public ActionResult AddForecast(ForecastRequestDto forecastDto)
        {
            try
            {
                _forecastRepo.AddForecast(forecastDto);

                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteForecast(int id)
        {
            try
            {
                _forecastRepo.DeleteForecast(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(new { message = $"Forecast with ID {id} not found: {ex.Message}" });
            }
        }
    }
}
    