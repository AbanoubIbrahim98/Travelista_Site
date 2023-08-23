using System.ComponentModel.DataAnnotations;

namespace Travelista.WeatherModel
{
    public class City
    {
        [Display(Name="City")]
        public string Name { get; set; }
        [Display(Name = "Temp.")]
        public float Temperature { get; set; }
        [Display(Name = "Humidity")]
        public int Humidity { get; set; }
        [Display(Name = "Pressure")]
        public int Pressure { get; set; }
        [Display(Name = "Wind Speed")]
        public float Wind { get; set; }
        [Display(Name = "weather Condition")]
        public string Weather { get; set; }
    }
}
