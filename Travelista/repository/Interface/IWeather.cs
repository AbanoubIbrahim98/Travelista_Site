using Travelista.WeatherModel;

namespace Travelista.repository.Interface
{
    public interface IWeather
    {
        Task<RootObject> GetWeather(string city);
    }
}
