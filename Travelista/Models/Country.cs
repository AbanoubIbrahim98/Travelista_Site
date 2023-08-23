using System.ComponentModel.DataAnnotations;

namespace Travelista.Models
{
    public class Country
    {
        public int ID { get; set; }
        [RegularExpression("^[a-zA-Z\\s]*$", ErrorMessage = "Please enter a valid country name.")]
        [Required]
        [MinLength(4)]
        public string Name { get; set; }
        [RegularExpression("^[A-Z]{3}$", ErrorMessage = "Please enter a valid currency code.")]
        public string? Currency { get; set; }
        [RegularExpression("^[1-9]\\d*$", ErrorMessage = "Please enter a valid population number.")]
        public int? Population { get; set; }
        [Display(Name="Image")]
        public string? Image_Url { get; set; }
       public  List<City>? Citys { get; set; }  
       
    }
}
