using Microsoft.EntityFrameworkCore;
using WeatherApp.Data;
using WeatherApp.DTOs.Weather;
using WeatherApp.Models; // Assuming the actual entity is in this namespace
using System.Linq;

namespace WeatherApp.Repos.WeatherRepo
{
    public class WeatherRepo : IWeatherRepo
    {
        private readonly ApplicationDbContext _context;

        public WeatherRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddWeather(WeatherResponseDto weatherDto)
        {
            var weather = new WeatherApp.Entity.Weather
            {
                Temperature = weatherDto.Temperature,
                Humidity = weatherDto.Humidity,
                WindSpeed = weatherDto.WindSpeed,
                Condition = weatherDto.Condition,
                CityId = weatherDto.CityId
            };
            _context.Weathers.Add(weather);
            _context.SaveChanges();
        }

        public WeatherResponseDto GetWeatherByCityId(int cityId)
        {
            var weather = _context.Weathers
                .Where(w => w.CityId == cityId)
                .Select(w => new WeatherResponseDto
                {
                    Temperature = w.Temperature,
                    Humidity = w.Humidity,
                    WindSpeed = w.WindSpeed,
                    Condition = w.Condition,
                    CityId = w.CityId
                })
                .FirstOrDefault();

            if (weather == null)
            {
                throw new Exception($"Weather data for City ID {cityId} not found.");
            }

            return weather;
        }

        public void UpdateWeather(int id, WeatherResponseDto weatherDto)
        {
            var weather = _context.Weathers.FirstOrDefault(w => w.Id == id);

            if (weather == null)
            {
                throw new Exception($"Weather data with ID {id} not found.");
            }
            
            weather.Temperature = weatherDto.Temperature;
            weather.Humidity = weatherDto.Humidity;
            weather.WindSpeed = weatherDto.WindSpeed;
            weather.Condition = weatherDto.Condition;
            weather.CityId = weatherDto.CityId;

            _context.Weathers.Update(weather);
            _context.SaveChanges();
        }
    }
}