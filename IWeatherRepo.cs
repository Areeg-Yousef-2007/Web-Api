using WeatherApp.DTOs.Weather;

namespace WeatherApp.Repos.WeatherRepo
{
    public interface IWeatherRepo
    {
        WeatherResponseDto GetWeatherByCityId(int cityId);
        void AddWeather(WeatherResponseDto weatherDto);
        void UpdateWeather(int id, WeatherResponseDto weatherDto);
    }
}
