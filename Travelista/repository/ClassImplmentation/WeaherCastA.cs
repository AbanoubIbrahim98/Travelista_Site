using System.Threading.Tasks;
using Travelista.repository.Interface;
using Travelista.WeatherModel;

namespace Travelista.repository.ClassImplmentation
{
    public class WeaherCastA : IWeather
    {
        public async Task<RootObject> GetWeather(string city)
        {


            HttpClient client = new HttpClient();

            try
            {
                var responce = await client.GetFromJsonAsync<RootObject>($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=b7720fbf37b5adb6e44ad6ddea9e03d1");


                return responce;


            }
            catch (HttpRequestException ex)
            {
                return null;
            }


        }


    }
}
